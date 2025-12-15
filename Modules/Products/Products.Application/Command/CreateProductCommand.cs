using MediatR;
using restatunt.Shared.DTOs.Products;

namespace Products.Application.Command;

public record CreateProductCommand(string Name, int Stock, decimal Price) : IRequest<ProductDto> { }