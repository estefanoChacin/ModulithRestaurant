using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Orders.Application.DTOs;
using Orders.Domain.Interfaces;

namespace Orders.Application.Queries
{
    public class GetAllOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<GetAllOrdersQueryHandler> logger) : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<GetAllOrdersQueryHandler> _logger = logger;

        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var listOrders = await _orderRepository.GetAllAsync();
            _logger.LogInformation("Se listan todas la ordenes.");
            return _mapper.Map<List<OrderDto>>(listOrders);
        }
    }
}
