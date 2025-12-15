using AutoMapper;
using Products.Application.Command;
using Products.Domain.Models;
using restatunt.Shared.DTOs.Products;

namespace Products.Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductCommand, ProductModel>();
        CreateMap<ProductModel, ProductDto>().ReverseMap();
    }
}
