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
    public class UserJsonRepository : JsonFileRepository<User>, IUserRepository
    {
        public UserJsonRepository(JsonContext jsonContext) : base(jsonContext)
        {
            jsonContext.LoadDataAsync<User>().Wait();
        }

        public async Task<long> CountUsers(PageRequest request)
        {
            PageResponse<User> response = new PageResponse<User>();
            var items = await GetAllAsync();
            long count = items.AsQueryable().ApplyFilter(request).Count();
            return count;
        }

        public async Task<PageResponse<User>> SearchUsers(PageRequest request)
        {
            PageResponse<User> response = new PageResponse<User>();
            var items = await GetAllAsync();
            response.ListResponseData = items.AsQueryable().ApplyFilter(request).ToList();
            return response;
        }
    }
}
