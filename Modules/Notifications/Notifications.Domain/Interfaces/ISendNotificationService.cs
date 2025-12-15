using Notifications.Domain.Models;

namespace Notifications.Domain.Interfaces;

public interface ISendNotificationService
{
    Task SendNotificationEmail(NotifiactionModel notification);
}
