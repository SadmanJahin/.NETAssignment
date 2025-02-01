using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.DataAccess.Data;
using Users.DataAccess.Factory;
using Users.DataAccess.Interfaces;
using Users.DataAccess.Repositories;

namespace Users.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DBConfig"));
            });

            services.AddScoped<JsonContext>(provider =>
            {
                string config = configuration.GetConnectionString("JsonFilePath");
                return new JsonContext(config);
            });
        }

        public static void AddUserRepositories(this IServiceCollection services)
        {
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
        }
    }
}
