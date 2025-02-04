using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Entities;
using WebApi.Shared.Models;

namespace Users.Application.Contracts
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserDto user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(long id);
        Task UpdateUserAsync(UserDto user);
        Task DeleteUserAsync(long id);
    }
}
