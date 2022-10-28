using Microsoft.AspNetCore.Identity;
using System;

namespace Messenger.DAL.Models
{
    public class UserEntity : IdentityUser
    {
        public string UserImagePath { get; set; }
    }
}
