using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.DataAccess.Data;
using Users.DataAccess.Interfaces;

namespace Users.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _transactionContext;
        private IDbContextTransaction _transaction;
        public UnitOfWork(DatabaseContext databaseContext)
        {
            _transactionContext = databaseContext;
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _transactionContext.Database.BeginTransactionAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _transactionContext.SaveChangesAsync();
        }
        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }
        public async Task RollBackAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}
