using BusinessLogicLayer.BasketService.Dtos;

namespace BusinessLogicLayer.BasketService;

public interface IBasketService
{
    Task<ServiceResult> CreateBasket(BasketCreateRequest request);
    Task<ServiceResult<BasketResponse>> GetBasket(Guid userId);
    Task<ServiceResult> DeleteBasket(Guid userId);
}