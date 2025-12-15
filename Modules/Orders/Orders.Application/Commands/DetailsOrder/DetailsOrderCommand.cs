using MediatR;
using Orders.Application.DTOs;

namespace Orders.Application.Commands.DetailsOrder
{
    public record DetailsOrderCommand(string IdOrder) : IRequest<DetailsOrderDto> { }
}
