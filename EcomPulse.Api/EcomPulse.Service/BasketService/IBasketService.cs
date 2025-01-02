using EcomPulse.Service.BasketService.Dtos;

namespace EcomPulse.Service.BasketService
{
    public interface IBasketService
    {
        Task<ServiceResult> CreateBasket(BasketCreateRequest request);
        Task<ServiceResult<BasketResponse>> GetBasket(Guid userId);
        Task<ServiceResult> DeleteBasket(Guid userId);
    }
}