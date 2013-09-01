using System;
using System.Web.Mvc;
using FPChat.Domain;
using FPChat.Domain.Entities;
using System.Web.Security;
using FPChat.Domain.Services.Interfaces;
using FPChat.Domain.Services.Concrete;

namespace FPChat.MvcClient.Controllers
{
    /// <summary>
    /// Default controller. 
    /// Responsible for displaying the main page for the 
    /// authenticated users.
    /// </summary>
    public class HomeController : Controller
    {
        private static readonly object locker = new object();

        private IUsersService usersService;
        private IMessagesService messagesService;
        public HomeController(IUsersService usersService, IMessagesService messagesService)
        {
            this.usersService = usersService;
            this.messagesService = messagesService;
        }

        public HomeController()
        {
            this.usersService = Bootstrapper.ServiceLocator.GetService<IUsersService>();
            this.messagesService = Bootstrapper.ServiceLocator.GetService<IMessagesService>();
        }

        /// <summary>
        /// Returns user to appropriate page depending on their authentication status.
        /// </summary>
        /// <returns>If user is authenticated he is redirected to main page,
        /// otherwise is redirected to login page.
        /// </returns>
        [Authorize]
        public ActionResult Index()
        {
            var currentUser = HttpContext.User.Identity;
            if (currentUser.IsAuthenticated && !ApplicationManager.IsUserAlreadyLogged(new ChatUser(currentUser.Name)))
            {
                //user is already logged, but his login doesn't exist 
                //in the global list of logged users
                //this occur in situation, when authentication cookie
                //was already available in the browser, so we must add the user manually
                //and add him to the global application cache
                //and mark old messages as readed
                lock (locker)
                {
                    ApplicationManager.LoggedUsers.Add(new ChatUser(Guid.NewGuid(), currentUser.Name));
                    usersService.AddUserToCache(currentUser.Name);
                    messagesService.MarkPreviousMessagesInCacheAsReceived(currentUser.Name);
                }
            }

            return View();
        }
    }
}
