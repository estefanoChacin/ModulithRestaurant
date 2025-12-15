using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Orders.Application.DTOs;
using Orders.Domain.Interfaces;
using Orders.Domain.Models;
using restatunt.Shared.DTOs.Products;
using restatunt.Shared.Events.Notifications;
using restatunt.Shared.Events.Products;
using restatunt.Shared.Queries.Customers;
using restatunt.Shared.Queries.Products;

namespace Orders.Application.Commands.CreateOrder;

public class CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IMediator mediator, ILogger<CreateOrderCommandHandler> logger) : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<CreateOrderCommandHandler> _logger = logger;

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inicia proceso de crear orden");
        //validar existencia de cliente
        var customer = await _mediator.Send(new GetCustomerByIdQuery(request.IdCustomer), cancellationToken).ConfigureAwait(false)
        ?? throw new Exception("cliente no existe");

        var order = _mapper.Map<OrderModel>(request);
        //validar existencia de productos
        var productsExists = await ValidateExistsProducts(order, _mediator, cancellationToken);
        ValidateProductsExhausted(order, productsExists);

        //Crear orden
        foreach (var item in order.Products)
        {
            var price = productsExists.Where(p => p.Id == item.Id).Select(p => p.Price).First();
            order.Total = +price * item.Quantity;
        }

        var orderCreated = await _orderRepository.CreateAsync(order);

        //Lanzar evento para ajustar stock de los productos
        await _mediator.Publish(new AdjustProductStockEvent
        (request.Products,
        false), cancellationToken);

        //Lanzar evento para enviar notificacion de la creacion de la orden
        await _mediator.Publish(new CreateNotificationEvent()
        {
            EmailCustomer = customer.Email,
            IdOrder = orderCreated.Id!,
            Total = orderCreated.Total
        }, cancellationToken);

        _logger.LogInformation("Finaliza proceso, respuesta: {@orderCreated}", orderCreated);
        return _mapper.Map<OrderDto>(orderCreated);
    }


    private static async Task<List<ProductDto>> ValidateExistsProducts(OrderModel order, IMediator _mediator, CancellationToken cancellationToken)
    {
        var listIds = order.Products.Select(p => p.Id).ToList();
        var productsExists = await _mediator.Send(new GetProductsByIdsQuery(listIds), cancellationToken).ConfigureAwait(false);

        if (listIds.Count != productsExists.Count)
            throw new Exception("No existen algunos productos");

        return productsExists;
    }

    private static void ValidateProductsExhausted(OrderModel order, List<ProductDto> productsExists)
    {
        //obtener productos que tengan un stock menor al solicitado en la orden
        var itemsOrder = order.Products;
        var productsExhausted = from product in productsExists
                                join item in itemsOrder on product.Id equals item.Id
                                where product.Stock < item.Quantity
                                select product;
        //validar que no hayan productos agotados.
        if (productsExhausted.Any())
            throw new Exception("productos sin stock " + productsExhausted.ToList());
    }
}
