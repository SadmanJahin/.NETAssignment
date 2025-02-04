using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Users.DataAccess.Data;
using Users.DataAccess.Interfaces;

namespace Users.DataAccess.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext _databaseContext;
        private DbSet<T> _dbSet => _databaseContext.Set<T>();
        public IQueryable<T> Entities => _dbSet;
        public GenericRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
