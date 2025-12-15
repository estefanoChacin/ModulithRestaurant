using MediatR;
using Microsoft.Extensions.Logging;
using Products.Domain.Interfaces;
using restatunt.Shared.Events.Products;

namespace Products.Application.Events;

public class AdjustProductStockEventHandler(IProductRepository productRepository, ILogger<AdjustProductStockEventHandler> logger) : INotificationHandler<AdjustProductStockEvent>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ILogger<AdjustProductStockEventHandler> _logger = logger;

    public async Task Handle(AdjustProductStockEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inicia proceso ajustar stock de los productos: {@notification}", notification);
        var Products = await _productRepository.GetProductsByIdsAsync([.. notification.Products.Select(p => p.Id)]);
        foreach (var product in Products)
        {
            var itemOrder = notification.Products.Where(p => p.Id == product.Id).First();
            if (notification.Discount)
                product.Stock += itemOrder.Quantity;
            else
                product.Stock -= itemOrder.Quantity;

            _ = await _productRepository.UpdateAsync(product).ConfigureAwait(false)
            ?? throw new Exception("Error actualizando stock de los productos");
        }
        _logger.LogInformation("Finaliza proceso");
    }
}
