using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EagleTech_Task.Application.Extensions
{
    public static class ServicesCollection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddMapping()
                .AddMediator()
                .AddValidators();

            return services;
        }

        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);

            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }

        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
