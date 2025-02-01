using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Entities;
using Users.DataAccess.Interfaces;
using Users.Application.Contracts;
using WebApi.Shared.Models;
using AutoMapper;
using Users.DataAccess.Factory;
using WebApi.Shared.Enums;

namespace Users.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IConnectionFactory connectionFactory, IMapper mapper)
        {
            _userRepository = connectionFactory.CreateUserRepository(StorageType.JSON);
            _unitOfWork = connectionFactory.CreateUnitOfWork(StorageType.JSON);
            _mapper = mapper;
        }
        public async Task<User> CreateUserAsync(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            await _userRepository.SaveAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return user;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            return await _userRepository.GetAsync(id);
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(long id)
        {
            User user = new User()
            {
                Id = id,
            };
            await _userRepository.DeleteAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
