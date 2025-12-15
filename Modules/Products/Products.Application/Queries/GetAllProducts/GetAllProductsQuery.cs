using MediatR;
using restatunt.Shared.DTOs.Products;

namespace Products.Application.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<List<ProductDto>> { }