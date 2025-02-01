using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.DataAccess.Data;
using Users.Application.Contracts;
using Users.Application.Services;

namespace Users.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddUserServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
