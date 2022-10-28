using System;
using System.ComponentModel.DataAnnotations;

namespace Messenger.BLL.Models
{
    public class Contact
    {
        public Guid ContactId { get; set; } = new Guid();
        public string OwnerId { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
    }
}
