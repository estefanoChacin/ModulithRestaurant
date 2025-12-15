using System.Diagnostics.CodeAnalysis;

namespace restatunt.Shared.DTOs.Order;

[ExcludeFromCodeCoverage]
public class ItemDto
{
    public required string Id { get; set; }
    public required int Quantity { get; set; } 
}

