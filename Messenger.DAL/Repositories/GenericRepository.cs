using Messenger.DAL.DataAccses;
using Messenger.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Messenger.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataAccsess _appData;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DataAccsess appData)
        {
            _appData = appData;
            _dbSet = appData.Set<T>();

        }

        public async Task<T> CreateAsync(T item)
        {
            _dbSet.Attach(item);
            var created = await _dbSet.AddAsync(item);
            await _appData.SaveChangesAsync();
            return created.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _dbSet.FindAsync(id);
            _dbSet.Remove(item);
            await _appData.SaveChangesAsync();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetPagedAsync(int page = 1, int pageSize = 15)
        {
            var amountToSkip = (page - 1) * pageSize;
            return await _dbSet.Skip(amountToSkip).AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T item)
        {
            _appData.Attach(item);
            _appData.Update(item);
            await _appData.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbSet.CountAsync();
        }

    }
}
