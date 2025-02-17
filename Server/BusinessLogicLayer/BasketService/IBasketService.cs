using BusinessLogicLayer.BasketService.Dtos;

namespace BusinessLogicLayer.BasketService;

public interface IBasketService
{
    Task<Result<string>> CreateBasket(BasketCreateRequest request);
    Task<Result<BasketResponse>> GetBasket(Guid userId);
    Task<Result<string>> DeleteBasket(Guid userId);
}