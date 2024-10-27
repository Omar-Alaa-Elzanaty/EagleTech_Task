using EagleTech_Task.Application.Interfaces.Auth;
using EagleTech_Task.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Infrastructure.Extenstions
{
    public static class ServicesCollection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCollections();


            return services;
        }

        private static IServiceCollection AddCollections(this IServiceCollection services)
        {
            services.AddTransient<IAuthServices, AuthService>();

            return services;
        }
    }
}
