using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.CategoryRepository;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly AppDbContext _context;
    public CategoryRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var response = await _context.Categories.ToListAsync();
        return response;
    }

    public async Task<Category> GetByIdAsync(Guid id)
    {
        var response = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        return response!;
    }
}
