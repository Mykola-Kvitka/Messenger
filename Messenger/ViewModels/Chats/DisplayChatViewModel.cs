using Messenger.BLL.Models;
using Messenger.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.PL.ViewModels
{
    public class DisplayChatViewModel
    {
        public Guid ChatId { get; set; }

        public string ChatName { get; set; }

        public List<MessegeViewModel> Messeges { get; set; }


    }
}
