using Messenger.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Interfaces
{
    public interface IContactService
    {
        Task<bool> CreateAsync(Contact request);
        Task<int> GetCountAsync();
        Task<IEnumerable<Contact>> GetPagedAsync(string UserId,  int page = 1, int pageSize = 20);
        void DeleteAsync(Guid id);
        Task<Contact> GetAsync(Guid id);
        void UpdateAsync(Contact request);
        Task<IEnumerable<Contact>> FindAsync(string UserId);


    }
}
