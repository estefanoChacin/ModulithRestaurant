using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Orders.Domain.Interfaces;
using restatunt.Shared.DTOs.Order;
using restatunt.Shared.Events.Products;

namespace Orders.Application.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IMediator mediator, IMapper mapper, IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger) : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly ILogger<DeleteOrderCommandHandler> _logger = logger;

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inicia proceso de eliminar orden, id: {id}", request.IdOrder);
        //verificar que la orden exista y obtener los productos de la orden
        var orderExists = await _orderRepository.GetByIdAsync(request.IdOrder) ?? throw new Exception("No existe Orden");
        var itemsOrder = _mapper.Map<List<ItemDto>>(orderExists.Products);
        //lanzar evento para ajustar el stock de los productos
        await _mediator.Publish(new AdjustProductStockEvent(
            itemsOrder,
            true), cancellationToken);
        //eliminar orden y retornar estado.
        var statusDelete = await _orderRepository.DeleteAsync(request.IdOrder).ConfigureAwait(false);
        _logger.LogInformation("Finaliza proceso, Respuesta: {statusDelete}", statusDelete);
        return statusDelete;
    }

}
