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
    public interface IMessegeService
    {
        Task CreateAsync(Messege request);
        Task<int> GetCountAsync();
        Task<IEnumerable<Messege>> GetPagedAsync(Guid ChatId,string userId, int page = 1, int pageSize = 20);
        Task DeleteAsync(Guid id);
        Task SoftDeleteAsync(Guid id);
        Task<Messege> GetAsync(Guid id);
        Task UpdateAsync(Messege request);
        Task<IEnumerable<Messege>> FindAsync(Expression<Func<MessegeEntity, bool>> predicate);

    }
}
