using MediatR;
using restatunt.Shared.DTOs.Customers;

namespace restatunt.Shared.Queries.Customers;

public record GetCustomerByIdQuery(string Id) : IRequest<CustomerDto> { }