namespace Notifications.Domain.Models;

public class NotifiactionModel
{
    public required string IdOrder { get; set; }
    public required string EmailCustomer { get; set; }
    public required decimal Total { get; set; }
}
