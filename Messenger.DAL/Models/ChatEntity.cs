using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Models
{
    public class ChatEntity
    {
        [Key]
        public Guid ChaiId { get; set; }
        public string ChatName { get; set; }
        public bool IsGroupChat { get; set; }
    }
}
