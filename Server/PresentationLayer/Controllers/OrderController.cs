using BusinessLogicLayer.OrderService;
using BusinessLogicLayer.OrderService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(OrderCreateRequest request)
    {
        var result = await orderService.CreateOrder(request);
        return result.IsSuccessful
             ? Ok(result)
             : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAll(Guid userId)
    {
        var result = await orderService.GetAllOrder(userId);
        return result.IsSuccessful
             ? Ok(result)
             : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpGet("OrderId/{orderId}")]
    public async Task<IActionResult> GetById(Guid orderId)
    {
        var result = await orderService.GetByIdOrder(orderId);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpDelete("{orderId}")]
    public async Task<IActionResult> Delete(Guid orderId)
    {
        var result = await orderService.OrderDelete(orderId);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
}
