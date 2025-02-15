using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;

namespace DataAccessLayer.BasketItemRepository;

public class BasketItemRepository(AppDbContext context) : GenericRepository<BasketItem>(context), IBasketItemRepository
{

}
