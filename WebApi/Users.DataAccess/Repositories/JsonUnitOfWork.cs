using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.DataAccess.Interfaces;

namespace Users.DataAccess.Repositories
{
    public class JsonUnitOfWork : IUnitOfWork
    {
        private readonly JsonContext _jsonContext;
        public JsonUnitOfWork(JsonContext jsonContext) 
        {
            _jsonContext = jsonContext;
        } 
        public async Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public async Task CommitAsync()
        {
           await _jsonContext.SaveChangesAsync();
        }

        public Task RollBackAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _jsonContext.SaveChangesAsync();
        }
    }
}
