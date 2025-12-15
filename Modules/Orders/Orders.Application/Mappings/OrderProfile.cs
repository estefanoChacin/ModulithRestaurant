using AutoMapper;
using Orders.Application.Commands.CreateOrder;
using Orders.Application.DTOs;
using Orders.Domain.Models;
using restatunt.Shared.DTOs.Order;

namespace Orders.Application.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderCommand, OrderModel>().ReverseMap();
        CreateMap<OrderDto, OrderModel>().ReverseMap();
        CreateMap<ItemDto, ItemModel>().ReverseMap();
    }
}
