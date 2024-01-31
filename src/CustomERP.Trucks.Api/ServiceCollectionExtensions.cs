using CustomERP.Trucks.Application.CreateTruck;
using CustomERP.Trucks.Domain;
using CustomERP.Trucks.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CustomERP.Trucks.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTrucksServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateTruckCommand>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetTrucksQueryCommandHandler>());

            services.AddScoped<ITruckRepository, TruckRepository>();
            services.AddScoped<ITruckCodeUniquenessConstraint, TruckRepository>();
            services.AddScoped<TrucksDbContext>();

            return services;
        }
    }
}
