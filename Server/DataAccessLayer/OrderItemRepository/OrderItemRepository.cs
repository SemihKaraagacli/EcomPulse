using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;

namespace DataAccessLayer.OrderItemRepository;

public class OrderItemRepository(AppDbContext context) : GenericRepository<OrderItem>(context), IOrderItemRepository
{
}
