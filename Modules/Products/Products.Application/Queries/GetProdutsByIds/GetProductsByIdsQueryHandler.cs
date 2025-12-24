using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Products.Domain.Interfaces;
using restatunt.Shared.DTOs.Products;
using restatunt.Shared.Queries.Products;

namespace Products.Application.Queries.GetProdutsByIds;

public class GetProductsByIdsQueryHandler(
    IProductRepository productRepository, 
    IMapper mapper, 
    ILogger<GetProductsByIdsQueryHandler> logger) 
    : IRequestHandler<GetProductsByIdsQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GetProductsByIdsQueryHandler> _logger = logger;

    public async Task<List<ProductDto>> Handle(GetProductsByIdsQuery request, CancellationToken cancellationToken)
    {
        var listResponse = await _productRepository
            .GetProductsByIdsAsync(request.ListIds)
            .ConfigureAwait(false);

        _logger.LogInformation("Se listan productos por lista de ids: {@Ids}", listResponse);
        return _mapper.Map<List<ProductDto>>(listResponse);
    }

}
