using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.DAL.DataAccses;
using Messenger.DAL.Interfaces;
using Messenger.DAL.Models;
using Messenger.DAL.Repositories;

namespace Messenger.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataAccsess _appData;

        private GenericRepository<MessegeEntity> _massages;
        private GenericRepository<ContactEntity> _contact;
        private GenericRepository<UserChatsEntity> _userChats;
        private GenericRepository<ChatEntity> _chats;
        private GenericRepository<UserEntity> _users;

        public UnitOfWork(DataAccsess appData)
        {
            _appData = appData;
        }

        public IGenericRepository<MessegeEntity> Messeges => _massages ??= new GenericRepository<MessegeEntity>(_appData);
        public IGenericRepository<UserEntity> Users => _users ??= new GenericRepository<UserEntity>(_appData);
        public IGenericRepository<ChatEntity> Chats => _chats ??= new GenericRepository<ChatEntity>(_appData);
        public IGenericRepository<ContactEntity> Contacts => _contact ??= new GenericRepository<ContactEntity>(_appData);
        public IGenericRepository<UserChatsEntity> UserChats => _userChats ??= new GenericRepository<UserChatsEntity>(_appData);

    }
}
