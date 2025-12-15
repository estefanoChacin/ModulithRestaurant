namespace Customers.Domain.Models;

public class CustomerModel
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
