using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.DAL.Models;

namespace Messenger.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<MessegeEntity> Messeges { get; }
        IGenericRepository<ContactEntity> Contacts { get; }
        IGenericRepository<UserChatsEntity> UserChats { get; }
        IGenericRepository<ChatEntity> Chats { get; }
        IGenericRepository<UserEntity> Users { get; }
    }
}
