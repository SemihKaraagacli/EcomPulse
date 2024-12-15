using EcomPulse.Service.OrderService;
using EcomPulse.Service.OrderService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    public class OrderController(IOrderService orderService) : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateRequest request)
        {
            var result = await orderService.CreateOrder(request);
            return CreateObjectResult(result);
        }
    }
}
