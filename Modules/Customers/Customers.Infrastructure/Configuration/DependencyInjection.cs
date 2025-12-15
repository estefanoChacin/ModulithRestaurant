using Customers.Domain.Interfaces;
using Customers.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddServicesCustomerInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, CustomerRepository>();
        return services;
    }
}
