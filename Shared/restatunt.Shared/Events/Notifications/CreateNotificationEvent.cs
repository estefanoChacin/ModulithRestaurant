using MediatR;

namespace restatunt.Shared.Events.Notifications;

public class CreateNotificationEvent: INotification
{
    public required string IdOrder { get; set; }
    public required string EmailCustomer { get; set; }
    public required decimal Total { get; set; }
}
