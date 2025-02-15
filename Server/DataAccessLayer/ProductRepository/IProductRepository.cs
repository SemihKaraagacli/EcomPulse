using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;

namespace DataAccessLayer.ProductRepository;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetFilterProductsAsync(Guid categoryId);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(Guid id);
}
