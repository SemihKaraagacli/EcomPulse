using EcomPulse.Repository.Entities;

namespace EcomPulse.Service.BasketService.Dtos
{
    public record BasketResponse(Guid Id, Guid UserId, List<BasketItem> BasketItems, decimal TotalPrice);
}
