using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace EcomPulse.Repository.CategoryRepository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public Task<Category> GetById(Guid id)
        {
            return _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
