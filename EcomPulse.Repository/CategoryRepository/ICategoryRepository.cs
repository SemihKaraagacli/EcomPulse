using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;

namespace EcomPulse.Repository.CategoryRepository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(Guid id);
    }
}
