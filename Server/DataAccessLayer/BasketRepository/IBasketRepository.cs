using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;

namespace DataAccessLayer.BasketRepository;

public interface IBasketRepository : IGenericRepository<Basket>
{
    Task<Basket> GetAllAsync(Guid userId);
}