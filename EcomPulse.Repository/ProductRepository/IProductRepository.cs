using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;

namespace EcomPulse.Repository.ProductRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetFilterProductsAsync(Guid categoryId);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid id);
    }
}
