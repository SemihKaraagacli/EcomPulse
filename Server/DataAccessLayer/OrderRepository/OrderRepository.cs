using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.OrderRepository;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    private readonly AppDbContext _context;
    public OrderRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Order>> GetAllOrder(Guid userId)
    {
        var response = await _context.Orders.Include(x => x.OrderItems!).ThenInclude(x => x.Product).Where(x => x.UserId == userId).ToListAsync();
        return response;
    }

    public async Task<Order> GetByIdOrder(Guid orderId)
    {
        var response = await _context.Orders.Include(x => x.OrderItems!).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == orderId);
        return response!;
    }
}

