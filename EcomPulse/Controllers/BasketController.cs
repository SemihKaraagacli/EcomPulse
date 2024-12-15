using EcomPulse.Service.BasketService;
using EcomPulse.Service.BasketService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    public class BasketController(IBasketService basketService) : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(BasketCreateRequest request)
        {
            var result = await basketService.CreateBasket(request);
            return CreateObjectResult(result);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var result = await basketService.GetBasket(userId);
            return CreateObjectResult(result);
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var result = await basketService.DeleteBasket(userId);
            return CreateObjectResult(result);
        }
    }
}
