using System.Collections.Generic;

namespace FPChat.MvcClient.Models
{
    public class LoggedUsersListModel
    {
        public IEnumerable<string> Users { set; get; }
    }
}