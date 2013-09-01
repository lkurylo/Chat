using System.Collections.Generic;
using System.Web.Mvc;
using FPChat.Domain.Entities;
using FPChat.Domain.Services.Interfaces;
using FPChat.MvcClient.Models;

namespace FPChat.MvcClient.Controllers
{
    /// <summary>
    /// Responsible for managing a users.
    /// </summary>
    public class UsersController : Controller
    {
        private IUsersService usersService;
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public UsersController()
        {
            this.usersService = Bootstrapper.ServiceLocator.GetService<IUsersService>();
        }

        /// <summary>
        /// Returns not authenticated users to the login form
        /// and authenticated to main page.
        /// </summary>
        /// <returns>Redirect users to appropriate pages depending on their authentication status.</returns>
        public ActionResult LogIn()
        {
            //this if statement is needed, because after new login is saved to db
            //user is redirected to main page, but I don't know yet why (TODO investigate it) this action
            //is executed too
            var currentUser = HttpContext.User.Identity;
            if (currentUser.IsAuthenticated)
            {
                //here show be redirect to the chat rooms, but I hadn't
                //time to implement the rooms functionality
                return RedirectToActionPermanent("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Responsible for logging out the current user.
        /// </summary>
        /// <returns>Redirect to the log in page.</returns>
        //[Authorize]
        public ActionResult LogOut()
        {
            string login = HttpContext.User.Identity.Name;

            //remove the user from the users list cache
            usersService.RemoveLogin(login);

            //and remove all his messages from cache/database
            usersService.RemoveUsersMessages(login);

            //remove the authentication cookie
            System.Web.Security.FormsAuthentication.SignOut();

            //and redirect user to the login page
            return RedirectToActionPermanent("LogIn", "Users");
        }

        /// <summary>
        /// Checks in database if specific login is already in use.
        /// </summary>
        /// <param name="login">The login to check</param>
        /// <returns>True if is use, otherwise false.</returns>
        public ActionResult CheckIfSpecifiedLoginIsUsed(string login)
        {
            bool result = usersService.IsLoginAlreadyInUse(login);

            return Json(result);
        }

        /// <summary>
        /// Adds new specific login to database.
        /// </summary>
        /// <param name="login">New login to save</param>
        /// <returns></returns>
        public ActionResult SaveLogin(string login)
        {
            ChatUser user = new ChatUser(login);

            //new login is saved in database, the user is authenticated, so 
            //set the auth cookie for him
            System.Web.Security.FormsAuthentication.SetAuthCookie(login, true);

            usersService.SaveNewLogin(user);

            return Json(true);
        }

        /// <summary>
        /// Returns logins list of currently logged users.
        /// </summary>
        /// <returns>List with logins.</returns>
        [Authorize]
        public ActionResult GetLoggedUsers()
        {
            var usersList = usersService.GetLoggedUsers();
            return PartialView(
                new LoggedUsersListModel()
            {
                Users = usersList
            });
        }
    }
}
