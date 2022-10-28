using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Models
{
    public class Chat
    {
        public Guid ChaiId { get; set; } = new Guid();
        public string ChatName { get; set; }
        public bool IsGroupChat { get; set; }
    }
}
