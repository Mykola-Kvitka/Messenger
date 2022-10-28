using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Models
{
    public class MessegeEntity
    {
        [Key]
        public Guid MessegeId { get; set; }
        public Guid ChatId { get; set; }
        public string OwnerId { get; set; }
        public string UserName { get; set; }
        [MaxLength(2000)]
        public string Massage { get; set; }
        public bool ISDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}
