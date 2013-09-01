using System;

namespace FPChat.Domain.Entities
{
    /// <summary>
    /// Represents a single chat user.
    /// </summary>
    public class ChatUser
    {
        #region constructors
       
        public ChatUser(string login)
        {
            Id = Guid.NewGuid();
            Login = login;

            //this is simplify
            //in real-world app, the timezones should be included
            //now the time is taken from server
            TimeLogged = DateTime.Now;
        }

        public ChatUser(Guid id, string login)
            : this(login)
        {
            Id = id;
        }

        #endregion

        #region properties
      
        /// <summary>
        /// User identificator. Must be unique per user.
        /// </summary>
        public Guid Id { private set; get; }

        /// <summary>
        /// User specified login. Must be unique across the application.
        /// </summary>
        public string Login { private set; get; }

        public DateTime TimeLogged { set; get; }

        #endregion
    }
}
