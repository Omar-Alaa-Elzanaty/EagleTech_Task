using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Presistance.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EagleTech_Task.Presistance.Extensions
{
    public static class ServicesCollection
    {
        public static IServiceCollection AddPresistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration)
                    .AddCollections();
            return services;
        }

        private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbServer");

            services.AddDbContext<OrderMangmentDbContext>(options =>
               options.UseLazyLoadingProxies().UseSqlServer(connectionString,
                   builder => builder.MigrationsAssembly(typeof(OrderMangmentDbContext).Assembly.FullName)));

            return services;
        }

        private static IServiceCollection AddCollections(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
