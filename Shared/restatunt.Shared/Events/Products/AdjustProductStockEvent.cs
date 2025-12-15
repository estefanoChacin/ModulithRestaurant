using MediatR;
using restatunt.Shared.DTOs.Order;

namespace restatunt.Shared.Events.Products;

public record AdjustProductStockEvent(List<ItemDto>Products, bool Discount): INotification{}
