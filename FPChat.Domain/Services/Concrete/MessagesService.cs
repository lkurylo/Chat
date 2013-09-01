using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FPChat.Domain.Entities;
using FPChat.Domain.Services.Interfaces;

namespace FPChat.Domain.Services.Concrete
{
    /// <summary>
    /// The concrete implementation of IMessagesService.
    /// </summary>
    public class MessagesService : IMessagesService
    {
        private static readonly object locker = new object();

        /// <summary>
        /// Adds new message to the cache/database.
        /// </summary>
        /// <param name="message">Message body.</param>
        public void AddNewMessage(string message)
        {
            string currentUser = HttpContext.Current.User.Identity.Name;

            ChatMessage m = new ChatMessage()
            {
                Content = message,
                Author = ApplicationManager.LoggedUsers.First(x => x.Login == currentUser)
                //new ChatUser(ApplicationManager.LoggedUsers[currentUser], currentUser)
            };

            //adds to Application object
            lock (locker)
            {
                ApplicationManager.Messages.Add(m);
            }

            //adds to db
        }

        /// <summary>
        /// Gets all the newest messages that the one specific.
        /// </summary>
        /// <param name="lastReceivedMessage">Id of the last received message.</param>
        /// <returns>List with messages.</returns>
        public IList<ChatMessage> GetNewestMessages(Guid lastReceivedMessage)
        {
            List<ChatMessage> newestMessages = null;
            string user = HttpContext.Current.User.Identity.Name;
            lock (locker)
            {
                try
                {
                    ChatMessage message = ApplicationManager.Messages.First(x => x.Id == lastReceivedMessage);
                    //  && x.Author.Login != user);
                    newestMessages = ApplicationManager.Messages
                       .Where(x => x.CreatedDate.CompareTo(message.CreatedDate) >= 0 && !ApplicationManager
                            .MessagesAlreadyReceived[user].Contains(x.Id) && x.Author.Login != user)
                       .Select(x => x).ToList();

                    return newestMessages.OrderBy(x => x.CreatedDate).ToList();
                }
                catch (InvalidOperationException)
                {
                    //bool exists = ApplicationManager.Messages.Contains(new ChatMessage() { Id = lastReceivedMessage });

                }
            }

            return newestMessages;
        }

        /// <summary>
        /// Gets first of the newest messages.
        /// </summary>
        /// <returns>Specific message.</returns>
        public ChatMessage GetFirstFromNewestMessages()
        {
            ChatMessage newestMessage = null;
            string user = HttpContext.Current.User.Identity.Name;
            lock (locker)
            {
                //try
                //{
                DateTime firstMessageDate;
                try
                {
                    if (ApplicationManager.Messages.Count > 0)
                    {
                        firstMessageDate = ApplicationManager.Messages
                            .Where(x => x.Author.Login != user
                                && !ApplicationManager
                                .MessagesAlreadyReceived[user].Contains(x.Id))
                            .Min(x => x.CreatedDate);

                        newestMessage = ApplicationManager.Messages
                                              .Where(x => x.CreatedDate.CompareTo(firstMessageDate) == 0).First();
                    }
                }
                catch (InvalidOperationException)
                {
                    //firstMessageDate = ApplicationManager.Messages
                    //    .Where(x => x.Author.Login != user)
                    //    .Min(x => x.CreatedDate);
                }
                //}
                //catch (InvalidOperationException)
                //{

                //}
            }

            return newestMessage;
        }

        /// <summary>
        /// Marks specific message as received by current logged user.
        /// </summary>
        /// <param name="messageId">Id of the message.</param>
        public void MarkMessageAsReceived(Guid messageId)
        {
            string user = HttpContext.Current.User.Identity.Name;

            lock (locker)
            {
                //if (!ApplicationManager.MessagesAlreadyReceived.Keys.Contains(user))
                //{
                //    List<Guid> ids = new List<Guid>();
                //    KeyValuePair<string, IList<Guid>> dict = new KeyValuePair<string, IList<Guid>>(user, ids);

                //    ApplicationManager.MessagesAlreadyReceived.Add(dict);
                //}

                ApplicationManager.MessagesAlreadyReceived[user].Add(messageId);
            }
        }

        /// <summary>
        /// Mark the messages in cache as received which were 
        /// added there before user logged in to the system.
        /// </summary>
        public void MarkPreviousMessagesInCacheAsReceived(string login)
        {
            string user = login;// HttpContext.Current.User.Identity.Name;
            DateTime currentTime = DateTime.Now;

            lock (locker)
            {
                ApplicationManager.Messages
                       .Where(x => x.CreatedDate.CompareTo(currentTime) < 0).Select(x => x.Id
                       ).ToList().ForEach(x =>
                       {
                           ApplicationManager.MessagesAlreadyReceived[user].Add(x);
                       });
            }
        }
    }
}
