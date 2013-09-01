using System;
using System.Collections.Generic;
using FPChat.Domain.Entities;

namespace FPChat.Domain.Services.Interfaces
{
    /// <summary>
    /// Basic interface for messages service.
    /// </summary>
    public interface IMessagesService
    {
        /// <summary>
        /// Adds new message to the cache/database.
        /// </summary>
        /// <param name="message">Message body.</param>
        void AddNewMessage(string message);

        /// <summary>
        /// Gets all the newest messages that the one specific.
        /// </summary>
        /// <param name="lastReceivedMessage">Id of the last received message.</param>
        /// <returns>List with messages.</returns>
        IList<ChatMessage> GetNewestMessages(Guid lastReceivedMessage);

        /// <summary>
        /// Gets first of the newest messages.
        /// </summary>
        /// <returns>Specific message.</returns>
        ChatMessage GetFirstFromNewestMessages();

        /// <summary>
        /// Marks specific message as received by current logged user.
        /// </summary>
        /// <param name="messageId">Id of the message.</param>
        void MarkMessageAsReceived(Guid messageId);

        /// <summary>
        /// Mark the messages in cache as received which were 
        /// added there before user logged in to the system.
        /// </summary>
        void MarkPreviousMessagesInCacheAsReceived(string login);
    }
}
