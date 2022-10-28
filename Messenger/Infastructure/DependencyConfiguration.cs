using Microsoft.Extensions.DependencyInjection;
using Messenger.BLL.Interfaces;
using Messenger.BLL.Models;
using Messenger.BLL.Services;
using Messenger.DAL;
using Messenger.DAL.Interfaces;
using Messenger.DAL.Models;
using Messenger.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Infastructure
{
    public static class DependencyConfiguration
    {
        public static void AddDependencies(this IServiceCollection service)
        {
            //DAL configuration
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddTransient<IGenericRepository<ChatEntity>, GenericRepository<ChatEntity>>();
            service.AddTransient<IGenericRepository<UserChatsEntity>, GenericRepository<UserChatsEntity>>();
            service.AddTransient<IGenericRepository<ContactEntity>, GenericRepository<ContactEntity>>();
            service.AddTransient<IGenericRepository<MessegeEntity>, GenericRepository<MessegeEntity>>();

            //BL configuration
            service.AddTransient<IChatServise, ChatServise>();
            service.AddTransient<IMessegeService, MessegeService>();
            service.AddTransient<IContactService, ContactService>();
        }

    }
}
