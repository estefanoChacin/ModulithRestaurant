using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Domain.Interfaces;
using Notifications.Domain.Models;
using Notifications.Infrastructure.Services;
using restatunt.Shared.Constans;

namespace Notifications.Infrastructure.Configuration;

public static class DependecyInjection
{
    public static IServiceCollection AddServicesInfrastructeNotifiactions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SettingsNotificationsModel>(configuration.GetSection(ConstantsServer.SECTION_SMPT));
        services.AddScoped<ISendNotificationService, SendNotificationService>();
        return services;
    }
}
