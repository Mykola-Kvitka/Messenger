using Messenger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Models
{
    public class CreateChat
    {
       public string ChatName { get; set; }
        public List<string> Users { get; set; }
        public List<UserEntity> User { get; set; }

    }
}
