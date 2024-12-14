using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace EcomPulse.Repository.BasketItemRepository
{
    public class BasketItemRepository(AppDbContext context) : GenericRepository<BasketItem>(context), IBasketItemRepository
    {

    }
}
