using System;
using System.ComponentModel.DataAnnotations;

namespace Messenger.DAL.Models
{
    public class ContactEntity
    {
        [Key]
        public Guid ContactId { get; set; }
        public string OwnerId { get; set; }
        public string UserId { get; set; }

    }
}
