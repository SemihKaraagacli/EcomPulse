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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await basketService.GetBasket(id);
            return CreateObjectResult(result);
        }
    }
}
