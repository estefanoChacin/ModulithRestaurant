using MediatR;
using Orders.Application.DTOs;

namespace Orders.Application.Queries
{
    public record GetAllOrdersQuery : IRequest<List<OrderDto>> { }
}
