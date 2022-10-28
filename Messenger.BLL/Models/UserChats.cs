using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Models
{
    public class UserChats
    {
        public Guid UserChatsId { get; set; } = new Guid();
        public string UserId { get; set; }
        public Guid ChatId { get; set; }
        public string ChatName { get; set; }

    }
}
