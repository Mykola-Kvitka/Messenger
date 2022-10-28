using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.PL.ViewModels
{
    public class ChatViewModel
    {
        public Guid ChaiId { get; set; } 
        public string ChatName { get; set; }
        public bool IsGroupChat { get; set; }
    }
}
