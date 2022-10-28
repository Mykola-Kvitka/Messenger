using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Models
{
    public class DisplayChat
    {
        public Guid ChatId { get; set; }

        public string ChatName { get; set; }

        public List<Messege> Messeges { get; set; }


    }
}
