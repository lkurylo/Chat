using System;

namespace FPChat.Domain.Entities
{
    /// <summary>
    /// Represents a PW.
    /// </summary>
    public class ChatPrivateMessage
    {
        /// <summary>
        /// Represents the message receiver.
        /// </summary>
        public ChatUser Recipient { set; get; }

        /// <summary>
        /// Contains the message basic info about it.
        /// </summary>
        public ChatMessage Message { set; get; }
    }
}
