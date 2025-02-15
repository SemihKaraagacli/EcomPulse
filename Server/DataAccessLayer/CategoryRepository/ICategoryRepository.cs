using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;

namespace DataAccessLayer.CategoryRepository;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> GetByIdAsync(Guid id);
}
