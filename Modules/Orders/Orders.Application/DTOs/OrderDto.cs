using restatunt.Shared.DTOs.Order;

namespace Orders.Application.DTOs;

public class OrderDto
{
    public required string Id { get; set; }
    public required List<ItemDto> Products { get; set; }
    public decimal Total { get; set; }
    public required string IdCustomer { get; set; }
}
