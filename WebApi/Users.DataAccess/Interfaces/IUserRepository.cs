using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Entities;

namespace Users.DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
