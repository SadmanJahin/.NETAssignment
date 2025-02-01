using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Entities;
using Users.DataAccess.Interfaces;
using WebApi.Shared.Models;

namespace Users.DataAccess.Repositories
{
    public class UserJsonRepository : JsonFileRepository<User> , IUserRepository
    {
        public UserJsonRepository(JsonContext jsonContext) : base(jsonContext)
        {
        }

        public Task<long> CountUsers(PageRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PageResponse<User>> SearchUsers(PageRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
