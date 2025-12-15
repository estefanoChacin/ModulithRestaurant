using AutoMapper;
using Customers.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using restatunt.Shared.DTOs.Customers;
using restatunt.Shared.Queries.Customers;

namespace Customers.Application.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper, ILogger<GetCustomerByIdQueryHandler> logger) : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GetCustomerByIdQueryHandler> _logger = logger;

    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inicia proceso de consultar cliente por id");
        var response = await _customerRepository.GetByIdAsync(request.Id).ConfigureAwait(false);
        _logger.LogInformation("Fianliza proceso, respuesta: {@response}", response);

        return _mapper.Map<CustomerDto>(response);
    }
}
