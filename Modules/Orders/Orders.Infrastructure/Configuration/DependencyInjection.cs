using Microsoft.Extensions.DependencyInjection;
using Orders.Domain.Interfaces;
using Orders.Infrastructure.Repository;

namespace Orders.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddServicesOrdersInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IOrderRepository, OrderRepository>();
        return services;
    }
}
