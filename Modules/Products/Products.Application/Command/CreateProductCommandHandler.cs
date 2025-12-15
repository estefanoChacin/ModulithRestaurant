using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Products.Domain.Interfaces;
using Products.Domain.Models;
using restatunt.Shared.DTOs.Products;

namespace Products.Application.Command;

public class CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<CreateProductCommandHandler> logger) : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<CreateProductCommandHandler> _logger = logger;


    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inicia proceso de crear producto");
        var response = await _productRepository.CreateAsync(_mapper.Map<ProductModel>(request));
        _logger.LogInformation("Finaliza proceso, respuesta: {@response}", response);
        return _mapper.Map<ProductDto>(response);
    }
}
