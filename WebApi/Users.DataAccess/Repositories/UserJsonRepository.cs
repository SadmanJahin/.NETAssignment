using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Entities;
using Users.DataAccess.Interfaces;

namespace Users.DataAccess.Repositories
{
    public class UserJsonRepository : JsonFileRepository<User> , IUserRepository
    {
        public UserJsonRepository(JsonContext jsonContext) : base(jsonContext)
        {
        }
    }
}
