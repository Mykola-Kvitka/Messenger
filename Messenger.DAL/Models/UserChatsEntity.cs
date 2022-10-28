using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Models
{
    public class UserChatsEntity
    {
        [Key]
        public Guid UserChatsId { get; set; }
        public string UserId { get; set; }
        public Guid ChatId { get; set; }
        public string ChatName { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
