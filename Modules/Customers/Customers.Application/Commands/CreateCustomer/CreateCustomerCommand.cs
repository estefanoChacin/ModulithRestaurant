using MediatR;
using restatunt.Shared.DTOs.Customers;

namespace Customers.Application.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<CustomerDto>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
}
