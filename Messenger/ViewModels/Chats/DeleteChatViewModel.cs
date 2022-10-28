using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.PL.ViewModels
{
    public class DeleteChatViewModel
    {
        public Guid ChaiId { get; set; }
        public string UserId { get; set; }
        public bool DeleteForOtherUser { get; set; } = false;

    }
}
