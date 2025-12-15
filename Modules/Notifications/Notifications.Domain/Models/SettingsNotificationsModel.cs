namespace Notifications.Domain.Models;

public class SettingsNotificationsModel
{
    public required string SmtpServer { get; set; }
    public required string Port { get; set; }
    public required string Password { get; set; }
    public required string EnableSsl { get; set; }
    public required string Email { get; set; }

}
