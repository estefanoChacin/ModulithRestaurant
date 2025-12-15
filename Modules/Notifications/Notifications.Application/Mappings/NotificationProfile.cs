using AutoMapper;
using Notifications.Domain.Models;
using restatunt.Shared.Events.Notifications;

namespace Notifications.Application.Mappings;

public class NotificationProfile:Profile
{
    public NotificationProfile()
    {
        CreateMap<CreateNotificationEvent, NotifiactionModel>().ReverseMap();
    }
}
