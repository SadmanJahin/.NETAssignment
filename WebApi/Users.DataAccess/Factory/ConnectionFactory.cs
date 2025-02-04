using Microsoft.Extensions.Configuration;
using Users.DataAccess.Data;
using Users.DataAccess.Interfaces;
using Users.DataAccess.Repositories;
using WebApi.Shared.Enums;

namespace Users.DataAccess.Factory
{
    public class ConnectionFactory : IConnectionFactory
    {
        private DatabaseContext _databaseContext;
        private JsonContext _jsonContext;
        public ConnectionFactory(DatabaseContext databaseContext, JsonContext jsonContext)
        {
            _databaseContext = databaseContext;
            _jsonContext = jsonContext;
        }

        public IUserRepository CreateUserRepository(StorageType type)
        {
            switch (type)
            {
                case StorageType.DB:
                    return new UserRepository(_databaseContext);
                case StorageType.JSON:
                    return new UserJsonRepository(_jsonContext);
                default:
                    return new UserRepository(_databaseContext);
            }
        }

        public IUnitOfWork CreateUnitOfWork(StorageType type)
        {
            switch (type)
            {
                case StorageType.DB:
                    return new UnitOfWork(_databaseContext);
                case StorageType.JSON:
                    return new JsonUnitOfWork(_jsonContext);
                default:
                    return new UnitOfWork(_databaseContext);
            }
        }

    }
}
