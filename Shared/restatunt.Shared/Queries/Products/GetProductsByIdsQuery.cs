using System.Diagnostics.CodeAnalysis;
using MediatR;
using restatunt.Shared.DTOs.Products;

namespace restatunt.Shared.Queries.Products;

[ExcludeFromCodeCoverage]
public record GetProductsByIdsQuery(List<string>ListIds): IRequest<List<ProductDto>>{}
