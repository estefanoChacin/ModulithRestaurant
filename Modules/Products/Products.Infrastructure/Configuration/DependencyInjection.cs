using Microsoft.Extensions.DependencyInjection;
using Products.Domain.Interfaces;
using Products.Infrastructure.Repository;

namespace Products.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddServicesProductsInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IProductRepository, ProductRepository>();
        return services;
    }
}
