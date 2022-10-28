using System;
using System.ComponentModel.DataAnnotations;

namespace Messenger.PL.ViewModels
{
    public class ContactViewModel
    {
        public Guid ContactId { get; set; }
        public string OwnerId { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
    }
}
