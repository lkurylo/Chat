using FPChat.Domain.Entities;
using System.Collections.Generic;

namespace FPChat.Domain.Services.Interfaces
{
    /// <summary>
    /// Basic interface for user service.
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Checks if specific user login is already in use.
        /// </summary>
        /// <param name="login">The user login to check</param>
        /// <returns>False if specific login is free, true otherwise.</returns>
        bool IsLoginAlreadyInUse(string login);

        /// <summary>
        /// Sets the new ChatUser to database.
        /// </summary>
        void SaveNewLogin(ChatUser user);

        /// <summary>
        /// Gets list containg logins of all currently logged users.
        /// </summary>
        /// <returns>List with logins.</returns>
        IEnumerable<string> GetLoggedUsers();

        /// <summary>
        /// Removes specific log from the Application object.
        /// </summary>
        /// <param name="login">User specific login to remove.</param>
        void RemoveLogin(string login);

        void RemoveUsersMessages(string login);

        void AddUserToCache(string user);
    }
}
