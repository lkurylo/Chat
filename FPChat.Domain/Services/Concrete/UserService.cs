using System;
using System.Collections.Generic;
using System.Linq;
using FPChat.Domain.Entities;
using FPChat.Domain.Services.Interfaces;
using FPChat.Domain.Utilities;

namespace FPChat.Domain.Services.Concrete
{
    /// <summary>
    /// The concrete implementation of IUserService
    /// </summary>
    public class UsersService : IUsersService
    {
        private static readonly object locker = new object();

        private IMessagesService messagesService;
        public UsersService(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        /// <summary>
        /// Checks if specific user login is already in use.
        /// </summary>
        /// <param name="login">The user login to check</param>
        /// <returns>False if specific login is free, true otherwise.</returns>
        public bool IsLoginAlreadyInUse(string login)
        {
            lock (locker)
            {
                //try
                //{
                bool result = ApplicationManager.LoggedUsers
                       .Contains(new ChatUser(login), new ChatUserComparerByLogin()); //.First(x=>x.Login==login);
                //}
                //catch (InvalidOperationException)
                //{
                //    return false;
                //}
                return result;
                //return true;
            }
        }

        /// <summary>
        /// Sets the new ChatUser to database.
        /// </summary>
        public void SaveNewLogin(ChatUser user)
        {
            lock (locker)
            {
                ApplicationManager.LoggedUsers.Add(user); //.Add(user.Login, user.Id);

                //TODO save to db

                //create the repository for messages for new user
               // string user = HttpContext.Current.User.Identity.Name;
                AddUserToCache(user.Login);
                messagesService.MarkPreviousMessagesInCacheAsReceived(user.Login);
            }
        }

        public void AddUserToCache(string user)
        {
            lock (locker)
            {
                if (!ApplicationManager.MessagesAlreadyReceived.Keys.Contains(user))
                {
                    List<Guid> ids = new List<Guid>();
                    KeyValuePair<string, IList<Guid>> dict = new KeyValuePair<string, IList<Guid>>(user, ids);

                    ApplicationManager.MessagesAlreadyReceived.Add(dict);
                }
            }
        }

        /// <summary>
        /// Gets list containg logins of all currently logged users.
        /// </summary>
        /// <returns>List with logins.</returns>
        public IEnumerable<string> GetLoggedUsers()
        {
            foreach (var singleKey in ApplicationManager.LoggedUsers.Select(x => x.Login)) //.Keys)
            {
                yield return singleKey;
            }
        }

        /// <summary>
        /// Removes specific log from the Application object.
        /// </summary>
        /// <param name="login">User specific login to remove.</param>
        public void RemoveLogin(string login)
        {
            lock (locker)
            {
                try
                {
                    var item = ApplicationManager.LoggedUsers.First(x => x.Login == login);
                    ApplicationManager.LoggedUsers.Remove(item);
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// Removes from the cache messages from specific user.
        /// </summary>
        /// <param name="login">User login.</param>
        public void RemoveUsersMessages(string login)
        {
            lock (locker)
            {
                ApplicationManager.Messages.Where(x => x.Author.Login == login)
                    .Select(x => x).ToList().ForEach(x =>
                    {
                        ApplicationManager.Messages.Remove(x);
                    });
            }
        }
    }
}
