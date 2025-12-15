using AutoMapper;
using Customers.Application.Commands.CreateCustomer;
using Customers.Domain.Models;
using restatunt.Shared.DTOs.Customers;

namespace Customers.Application.Mappings;

public class CustomerProfile:Profile
{
    public CustomerProfile()
    {
        CreateMap<CreateCustomerCommand, CustomerModel>();
        CreateMap<CustomerModel, CustomerDto>().ReverseMap();
    }
}
