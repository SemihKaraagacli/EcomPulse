using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace EcomPulse.Repository.OrderRepository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAllOrder(Guid userId)
        {
            return await _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).Where(x => x.UserId == userId).ToListAsync();
        }
        public async Task<Order> GetByIdOrder(Guid userId)
        {
            return await _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
