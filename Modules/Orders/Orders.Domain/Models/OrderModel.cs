namespace Orders.Domain.Models;

public class OrderModel
{
    public string? Id { get; set; }
    public required List<ItemModel> Products{ get; set; }
    public decimal Total  { get; set; }
    public required string IdCustomer { get; set; }
}
