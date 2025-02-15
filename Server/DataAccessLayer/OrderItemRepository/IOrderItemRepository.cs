using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;

namespace DataAccessLayer.OrderItemRepository;

public interface IOrderItemRepository : IGenericRepository<OrderItem>
{
}
