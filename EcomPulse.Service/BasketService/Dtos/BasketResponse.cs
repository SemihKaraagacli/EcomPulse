using EcomPulse.Repository.Entities;

namespace EcomPulse.Service.BasketService.Dtos
{
    public record BasketResponse(Guid Id, Guid UserId, List<BasketItemResponse> BasketItems, decimal TotalPrice);
}
