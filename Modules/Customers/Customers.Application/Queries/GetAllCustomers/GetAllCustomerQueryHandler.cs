using AutoMapper;
using Customers.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using restatunt.Shared.DTOs.Customers;

namespace Customers.Application.Queries.GetAllCustomers;

public class GetAllCustomerQueryHandler(IMapper mapper, ICustomerRepository customerRepository, ILogger<GetAllCustomerQueryHandler> logger) : IRequestHandler<GetAllCustomerQuery, List<CustomerDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly ILogger<GetAllCustomerQueryHandler> _logger = logger;

    public async Task<List<CustomerDto>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var ListCustomersModels = await _customerRepository.GetAllAsync().ConfigureAwait(false);
        _logger.LogInformation("Se listan todos los clientes");
        return _mapper.Map<List<CustomerDto>>(ListCustomersModels);
    }

}
