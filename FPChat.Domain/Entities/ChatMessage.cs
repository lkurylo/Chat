using System;
using System.Web;

namespace FPChat.Domain.Entities
{
    /// <summary>
    /// Represents a single chat message.
    /// </summary>
    [Serializable]
    public class ChatMessage
    {
        public ChatMessage()
        {
            Id = Guid.NewGuid();

            //this is simplify
            //in real-world app, the timezones should be included
            //now the time is taken from server
            CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// Gets the message identificator. Must be unique per each item.
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// Gets/sets the message body.
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// Gets the sender (author) of the message.
        /// </summary>
        public ChatUser Author { set; get; }

        public DateTime CreatedDate { private set; get; }
    }
}
