using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Products.Domain.Interfaces;
using restatunt.Shared.DTOs.Products;

namespace Products.Application.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(
    IProductRepository productRepository, 
    IMapper mapper, 
    ILogger<GetAllProductsQueryHandler> logger) 
    : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GetAllProductsQueryHandler> _logger = logger;

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var listProducts = await _productRepository
            .GetAllAsync()
            .ConfigureAwait(false);

        _logger.LogInformation("Se listan todos los productos");
        return _mapper.Map<List<ProductDto>>(listProducts);
    }

}
