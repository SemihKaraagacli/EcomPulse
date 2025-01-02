using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;

namespace EcomPulse.Repository.BasketRepository
{
    public interface IBasketRepository : IGenericRepository<Basket>
    {
        Task<Basket> GetAllAsync(Guid userId);
    }
}