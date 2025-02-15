using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;

namespace DataAccessLayer.OrderRepository;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetAllOrder(Guid userId);
    Task<Order> GetByIdOrder(Guid orderId);
}