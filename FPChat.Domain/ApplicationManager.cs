using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FPChat.Domain.Entities;
using FPChat.Domain.Utilities;

namespace FPChat.Domain
{
    /// <summary>
    /// Responsible for managing the global Application object
    /// in entire application.
    /// </summary>
    public static class ApplicationManager
    {
        private static HttpApplicationState application;
        private static readonly object locker = new object();

        static ApplicationManager()
        {
            ApplicationManager.application = HttpContext.Current.Application;
        }

        /// <summary>
        /// Contains list of currently logged users in the application.
        /// </summary>
        public static IList<ChatUser> LoggedUsers
        {
            get
            {
                lock (locker)
                {
                    if (application["LoggedUsers"] == null)
                    {
                        application["LoggedUsers"] = new List<ChatUser>();
                    }

                    return (IList<ChatUser>)application["LoggedUsers"];
                }
            }
        }

        /// <summary>
        /// Contains list of all messages send in the main chat window
        /// by all logged users.
        /// </summary>
        public static IList<ChatMessage> Messages
        {
            get
            {
                lock (locker)
                {
                    if (application["Messages"] == null)
                    {
                        application["Messages"] = new List<ChatMessage>();
                    }

                    return (IList<ChatMessage>)application["Messages"];
                }
            }
        }

        /// <summary>
        /// Contains list of currently received messages for each logged user.
        /// </summary>
        public static IDictionary<string, IList<Guid>> MessagesAlreadyReceived
        {
            get
            {
                lock (locker)
                {
                    if (application["MessagesAlreadyReceived"] == null)
                    {
                        application["MessagesAlreadyReceived"] = new Dictionary<string, IList<Guid>>();
                    }

                    return (IDictionary<string, IList<Guid>>)application["MessagesAlreadyReceived"];
                }
            }
        }

        /// <summary>
        /// Checks if specific user is already logged.
        /// </summary>
        /// <param name="login">User login to check</param>
        /// <returns>True if user login is already used, false otherwise.</returns>
        public static bool IsUserAlreadyLogged(ChatUser user)
        {
            lock (locker)
            {
                var result = LoggedUsers.Contains(user, new ChatUserComparerByLogin());

                return result;
            }
        }
    }
}