using System;
using System.Web.Mvc;
using FPChat.Domain.Services.Interfaces;

namespace FPChat.MvcClient.Controllers
{
    /// <summary>
    /// Responsible for managing a messages.
    /// </summary>
    public class MessagesController : Controller
    {
        private IMessagesService messagesService;
        public MessagesController(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        public MessagesController()
        {
            this.messagesService = Bootstrapper.ServiceLocator.GetService<IMessagesService>();
        }

        /// <summary>
        /// Adds new message to database.
        /// </summary>
        /// <param name="message">Message body</param>
        /// <returns>Executes a javascript method responsible
        /// for adding this message to the textarea located on the main page.
        /// </returns>
        [Authorize]
        public ActionResult AddNew(string message)
        {
            string encodedMessage = message
                .Trim()
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");

            messagesService.AddNewMessage(encodedMessage);
            string user = this.HttpContext.User.Identity.Name;

            return JavaScript("FPChat.MessagesAndUsers.appendNewLine(\"" + String.Format("{0}({2}): {1}", user,
                encodedMessage, DateTime.Now.ToLongTimeString()) + "\");");
        }

        /// <summary>
        /// Gets the newest messages.
        /// </summary>
        /// <param name="lastReceivedMessage">Last received message id.</param>
        /// <returns>List with messages.</returns>
        [Authorize]
        public ActionResult GetNewestMessages(Guid lastReceivedMessage)
        {
            var messages = messagesService.GetNewestMessages(lastReceivedMessage);
            if (messages != null)
            {
                return Json(messages);
            }
            else
            {
                return Json("empty");
            }
        }

        /// <summary>
        /// Gets the first from the newest messages.
        /// </summary>
        /// <returns>Newest message.</returns>
        [Authorize]
        public ActionResult GetFirstFromNewestMessages()
        {
            var message = messagesService.GetFirstFromNewestMessages();

            if (message != null)
            {             
                string result =
                   String.Format("FPChat.MessagesAndUsers.markerTheMessageAsLast(\"{0}\");FPChat.MessagesAndUsers.appendNewLine(\"{1}({2}): {3}\")",
                   message.Id.ToString(),
                   message.Author.Login, message.CreatedDate.ToLongTimeString(), message.Content);

                return JavaScript(result);
            }
            else
            {
                return Json("empty");
            }
        }

        /// <summary>
        /// Gets html for the private message box.
        /// </summary>
        /// <returns>Private message box in html.</returns>
        [Authorize]
        public ActionResult PrivateMessageBox()
        {
            return PartialView();
        }

        [Authorize]
        public void MarkMessageAsReceived(Guid messageId)
        {
            messagesService.MarkMessageAsReceived(messageId);
        }
    }
}
