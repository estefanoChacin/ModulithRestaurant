using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Notifications.Domain.Interfaces;
using Notifications.Domain.Models;
using restatunt.Shared.Events.Notifications;

namespace Notifications.Application.Events;

public class CreateNotificationEventHandler(
    ISendNotificationService notificationService, 
    IMapper mapper, 
    ILogger<CreateNotificationEventHandler> logger) 
    : INotificationHandler<CreateNotificationEvent>
{
    private readonly ISendNotificationService _notificationService = notificationService;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<CreateNotificationEventHandler> _logger = logger;

    public async Task Handle(CreateNotificationEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inicia proceso de enviar notificacion para {Email}", notification.EmailCustomer);
        await _notificationService.SendNotificationEmail(new NotifiactionModel()
        {
            EmailCustomer = notification.EmailCustomer,
            IdOrder = notification.IdOrder,
            Total = notification.Total
        });
        _logger.LogInformation("Finaliza proceso");

    }

}
