using Customers.Domain.Models;

namespace Customers.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<CustomerModel> CreateAsync(CustomerModel customer);
    Task<CustomerModel> UpdateAsync(CustomerModel customer);
    Task<bool> DeleteAsync(string id);
    Task<List<CustomerModel>> GetAllAsync();
    Task<CustomerModel> GetByIdAsync(string id);
}
