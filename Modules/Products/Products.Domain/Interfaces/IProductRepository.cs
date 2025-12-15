using Products.Domain.Models;

namespace Products.Domain.Interfaces;

public interface IProductRepository
{
    Task<ProductModel> CreateAsync(ProductModel product);
    Task<ProductModel> UpdateAsync(ProductModel product);
    Task<bool> DeleteAsync(string id);
    Task<List<ProductModel>> GetAllAsync();
    Task<ProductModel> GetByIdAsync(string id);
    Task<List<ProductModel>> GetProductsByIdsAsync(List<string> ids);
}
