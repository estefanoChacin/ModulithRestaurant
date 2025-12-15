using AutoMapper;
using Customers.Domain.Models;
using Customers.Infrastructure.Entities;

namespace Customers.Infrastructure.Mappings;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerModel, CustomerEntity>().ReverseMap();
    }
}
