using MediatR;
using Orders.Application.DTOs;
using restatunt.Shared.DTOs.Order;

namespace Orders.Application.Commands.CreateOrder;

public record CreateOrderCommand(List<ItemDto> Products, string IdCustomer) : IRequest<OrderDto> { }
