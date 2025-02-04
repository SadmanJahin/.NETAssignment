using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.DataAccess.Interfaces;
using WebApi.Shared.Enums;

namespace Users.DataAccess.Factory
{
    public interface IConnectionFactory
    {
        IUserRepository CreateUserRepository(StorageType type);
        IUnitOfWork CreateUnitOfWork(StorageType type);
    }
}
