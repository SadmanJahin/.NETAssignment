using Azure;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Entities;
using Users.DataAccess.Data;
using Users.DataAccess.Extensions;
using Users.DataAccess.Interfaces;
using WebApi.Shared.Models;

namespace Users.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DatabaseContext _databaseContext;
        public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public override async Task<User> GetAsync(long id)
        {
            return await _databaseContext.Users.Include(item => item.Contact).Include(item => item.Role).FirstOrDefaultAsync(item => item.Id == id);
        }
        public async Task<long> CountUsers(PageRequest request)
        {
            var query = _databaseContext.Users.AsNoTracking().ApplyFilter(request);
            long count = await query.CountAsync();
            return count;
        }
        
        public async Task<PageResponse<User>> SearchUsers(PageRequest request)
        {
            PageResponse<User> response = new PageResponse<User>();
            var query = _databaseContext.Users.AsNoTracking().ApplyFilter(request);
            response.ListResponseData = await query.ToListAsync();
            return response;
        }
    }
}
