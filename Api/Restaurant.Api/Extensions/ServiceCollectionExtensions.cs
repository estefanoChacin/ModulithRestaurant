using Azure.Identity;
using Customers.Infrastructure.Configuration;
using Microsoft.OpenApi.Models;
using Notifications.Infrastructure.Configuration;
using Orders.Infrastructure.Configuration;
using Products.Infrastructure.Configuration;
using restatunt.Shared.Constans;

namespace Restaurant.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesModules(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        #region Libreries
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        #endregion

        #region Modules Services
        services.AddServicesCustomerInfrastructure();
        services.AddServicesOrdersInfrastructure();
        services.AddServicesProductsInfrastructure();
        services.AddServicesInfrastructeNotifiactions(configuration);
        #endregion
        return services;
    }

    public static IServiceCollection AddAppConfigKeyVault(
        this IServiceCollection services, 
        IConfigurationBuilder configuration)
    {
        configuration.AddAzureAppConfiguration(options =>
        {
            options.Connect(new Uri(GetEnviromentURL()), new DefaultAzureCredential())
            .Select("*", "Restaurant") // O puedes seleccionar por etiquetas o prefijos
            .ConfigureKeyVault(kv =>
            {
                kv.SetCredential(new DefaultAzureCredential());
            });
        });
        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Restaurant",
                Version = "v1",
                Description = "api web"
            });
        });
        return services;
    }

    private static string GetEnviromentURL()
        => Environment.GetEnvironmentVariable(ConstantsServer.URL_APPCONFIG) 
        ?? throw new ArgumentException(nameof(Environment));
}
