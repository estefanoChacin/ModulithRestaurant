namespace Products.Domain.Models;

public class ProductModel
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required int Stock { get; set; }
    public required decimal Price { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
}
