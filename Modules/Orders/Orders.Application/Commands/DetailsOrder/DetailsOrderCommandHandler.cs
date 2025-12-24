
using MediatR;
using Microsoft.Extensions.Logging;
using Orders.Application.DTOs;
using Orders.Domain.Interfaces;
using restatunt.Shared.Queries.Customers;
using restatunt.Shared.Queries.Products;

namespace Orders.Application.Commands.DetailsOrder
{
    public class DetailsOrderCommandHandler(
        IOrderRepository orderRepository,
        IMediator mediator,
        ILogger<DetailsOrderCommandHandler> logger)
        : IRequestHandler<DetailsOrderCommand, DetailsOrderDto>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<DetailsOrderCommandHandler> _logger = logger;

        public async Task<DetailsOrderDto> Handle(DetailsOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Inicia proceso Obtener detalles de orden para: {Id}", request.IdOrder);
            var order = await _orderRepository.GetByIdAsync(request.IdOrder)
                ?? throw new KeyNotFoundException("Order not found");

            var idsProducts = order.Products.Select(p => p.Id).ToList();

            var listProductss = await _mediator.Send(
                new GetProductsByIdsQuery(idsProducts),
                cancellationToken);

            var customer = await _mediator.Send(
                new GetCustomerByIdQuery(order.IdCustomer),
                cancellationToken);

            var orderDetail = new DetailsOrderDto()
            {
                IdOrder = order.Id!,
                NameCustomer = customer.Name!,
                Products = listProductss,
                Total = order.Total
            };

            _logger.LogInformation("Finaliza proceso.");
            return orderDetail;
        }
    }
}
