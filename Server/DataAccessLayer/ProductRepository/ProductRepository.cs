using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.ProductRepository;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var response = await _context.Products.Include(x => x.Category).ToListAsync();
        return response;
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        var response = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        return response!;
    }

    public async Task<IEnumerable<Product>> GetFilterProductsAsync(Guid categoryId)
    {
        var response = await _context.Products.Include(x => x.Category).Where(x => x.CategoryId == categoryId).ToListAsync();
        return response;
    }
}
