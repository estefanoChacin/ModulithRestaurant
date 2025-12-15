using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Notifications.Domain.Interfaces;
using Notifications.Domain.Models;

namespace Notifications.Infrastructure.Services;

public class SendNotificationService(IOptions<SettingsNotificationsModel> options) : ISendNotificationService
{
    private readonly IOptions<SettingsNotificationsModel> _options = options;


    public async Task SendNotificationEmail(NotifiactionModel notification)
    {
        var client = new SmtpClient(_options.Value.SmtpServer)
        {
            Port = Convert.ToInt32(_options.Value.Port),
            Credentials = new NetworkCredential(_options.Value.Email, _options.Value.Password),
            EnableSsl = Convert.ToBoolean(_options.Value.EnableSsl) // ¡Importante!
        };

        var body = string.Format(GetContentTemplateOrderCreated(), notification.IdOrder, notification.Total);
        var message = new MailMessage
        {
            From = new MailAddress(_options.Value.Email),
            Subject = "Notificacion de orden.",
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(notification.EmailCustomer);
        await client.SendMailAsync(message);
    }

    private static string GetContentTemplateOrderCreated()
    {
        var template = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Templates", "EmailOrder.html"));
        return template;
    }

}
