using System.Collections.Generic;
using FPChat.Domain.Entities;

namespace FPChat.Domain.Utilities
{
    /// <summary>
    /// Helper class for comparing ChatUser's objects by their logins.
    /// </summary>
    public class ChatUserComparerByLogin : IEqualityComparer<ChatUser>
    {
        public bool Equals(ChatUser x, ChatUser y)
        {
            if (x.Login == y.Login)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(ChatUser obj)
        {
            return obj.Id.GetHashCode() ^ obj.Login.GetHashCode() ^ obj.TimeLogged.GetHashCode();
        }
    }
}
