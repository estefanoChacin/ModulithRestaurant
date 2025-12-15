using AutoMapper;
using Products.Domain.Models;
using Products.Infrastructure.Entities;

namespace Products.Infrastructure.Mappings;

public class ProfileMapping:Profile
{
    public ProfileMapping()
    {
        CreateMap<ProductModel, ProductEntity>().ReverseMap();
    }
}
