using EcomPulse.Service.OrderService;
using EcomPulse.Service.OrderService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    [Authorize(AuthenticationSchemes = "SigninToken")]
    public class OrderController(IOrderService orderService) : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateRequest request)
        {
            var result = await orderService.CreateOrder(request);
            return CreateObjectResult(result);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAll(Guid userId)
        {
            var result = await orderService.GetAllOrder(userId);
            return CreateObjectResult(result);
        }
        [HttpGet("OrderId/{orderId}")]
        public async Task<IActionResult> GetById(Guid orderId)
        {
            var result = await orderService.GetByIdOrder(orderId);
            return CreateObjectResult(result);
        }
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> Delete(Guid orderId)
        {
            var result = await orderService.OrderDelete(orderId);
            return CreateObjectResult(result);
        }
    }
}
