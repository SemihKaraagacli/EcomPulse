using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.BasketRepository;

public class BasketRepository : GenericRepository<Basket>, IBasketRepository
{
    private readonly AppDbContext _context;
    public BasketRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Basket> GetAllAsync(Guid userId)
    {
        var response = await _context.Baskets.Include(x => x.BasketItems!).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.UserId == userId);
        return response!;
    }
}
