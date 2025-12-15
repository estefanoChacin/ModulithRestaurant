using MediatR;

namespace Orders.Application.Commands.DeleteOrder;

public record DeleteOrderCommand(string IdOrder):IRequest<bool>{}
