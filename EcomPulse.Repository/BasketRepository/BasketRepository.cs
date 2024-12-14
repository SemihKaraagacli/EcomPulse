using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace EcomPulse.Repository.BasketRepository
{
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
        private readonly AppDbContext _context;
        public BasketRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Basket> GetAllAsync(Guid userId)
        {
            return await _context.Baskets.Include(x => x.BasketItems).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
