using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Messenger.DAL.Models;

namespace Messenger.DAL.DataAccses
{
    public class DataAccsess : IdentityDbContext<UserEntity, RoleEntity, string>
    {
        public DataAccsess(DbContextOptions<DataAccsess> options) : base(options) { Database.EnsureCreated(); }

        public DbSet<ChatEntity> Chats { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<UserChatsEntity> UserChats { get; set; }
        public DbSet<MessegeEntity> Massages { get; set; }
    }
}
