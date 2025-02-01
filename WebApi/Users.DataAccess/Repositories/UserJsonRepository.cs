using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Entities;

namespace Users.DataAccess.Repositories
{
    public class UserJsonRepository : JsonFileRepository<User>
    {
        public UserJsonRepository(string filePath, JsonContext jsonContext) : base(filePath, jsonContext)
        {
        }
    }
}
