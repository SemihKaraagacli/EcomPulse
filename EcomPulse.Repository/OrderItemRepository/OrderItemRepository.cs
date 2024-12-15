using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;

namespace EcomPulse.Repository.OrderItemRepository
{
    public class OrderItemRepository(AppDbContext context) : GenericRepository<OrderItem>(context), IOrderItemRepository
    {
    }
}
