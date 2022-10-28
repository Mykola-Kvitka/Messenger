using Messenger.BLL.Models;
using Messenger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Interfaces
{
    public interface IChatServise
    {
        Task CreateAsync(CreateChat chats);
         void DeleteAsync(DeleteChat del);
         Task<IEnumerable<Chat>> FindAsync(Expression<Func<Chat, bool>> predicate);
         Task<IEnumerable<DisplayChats>> GetAllAsync(string Userid);
        Task<DisplayChat> GetAsync(Guid id, string userId);


    }
}
