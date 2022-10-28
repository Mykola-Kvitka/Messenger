using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.PL.ViewModels
{
    public class UserChatsViewModel
    {
        public Guid UserChatsId { get; set; }
        public string UserId { get; set; }
        public Guid ChatId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string ChatName { get; set; }

    }
}
