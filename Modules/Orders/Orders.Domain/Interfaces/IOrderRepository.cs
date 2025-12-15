using Orders.Domain.Models;

namespace Orders.Domain.Interfaces;

public interface IOrderRepository
{
    Task<OrderModel> CreateAsync(OrderModel order);
    Task<OrderModel> UpdateAsync(OrderModel order);
    Task<bool> DeleteAsync(string id);
    Task<List<OrderModel>> GetAllAsync();
    Task<OrderModel> GetByIdAsync(string id);
}
