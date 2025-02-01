using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Entities;
using WebApi.Shared.Models;

namespace Users.DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<long> CountUsers(PageRequest request);
        Task<PageResponse<User>> SearchUsers(PageRequest request);
    }
}
