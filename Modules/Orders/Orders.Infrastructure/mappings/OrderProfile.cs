using AutoMapper;
using Orders.Domain.Models;
using Orders.Infrastructure.Entities;

namespace Orders.Infrastructure.mappings;

public class OrderProfile:Profile
{
    public OrderProfile()
    {
        CreateMap<ItemModel, ItemEntity>().ReverseMap();
        CreateMap<OrderModel, OrderEntity>().ReverseMap();
    }
}
