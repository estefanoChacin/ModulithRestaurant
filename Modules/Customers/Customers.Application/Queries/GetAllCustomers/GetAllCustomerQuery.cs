using MediatR;
using restatunt.Shared.DTOs.Customers;

namespace Customers.Application.Queries.GetAllCustomers;

public record GetAllCustomerQuery:IRequest<List<CustomerDto>>{}
