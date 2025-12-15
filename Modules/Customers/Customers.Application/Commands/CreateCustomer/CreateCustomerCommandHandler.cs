using AutoMapper;
using Customers.Domain.Interfaces;
using Customers.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using restatunt.Shared.DTOs.Customers;

namespace Customers.Application.Commands.CreateCustomer;

public class CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper, ILogger<CreateCustomerCommandHandler> logger) : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<CreateCustomerCommandHandler> _logger = logger;

    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inicia proceso Crear Cliente");
        var response = await _customerRepository.CreateAsync(_mapper.Map<CustomerModel>(request)).ConfigureAwait(false);
        _logger.LogInformation("Finaliza proceso, respuesta: {@response}",response);
        return _mapper.Map<CustomerDto>(response);
    }
}
