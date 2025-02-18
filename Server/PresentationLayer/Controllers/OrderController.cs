using BusinessLogicLayer.OrderService;
using BusinessLogicLayer.OrderService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    [HttpPost]
    public async Task<IActionResult> Create(OrderCreateRequest request)
    {
        Logger.Info("CreateOrder endpoint called with data: {Data}", request);

        var result = await orderService.CreateOrder(request);

        if (result.IsSuccessful)
        {
            Logger.Info("Order created successfully with data: {Data}", request);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("CreateOrder failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAll(Guid userId)
    {
        Logger.Info("GetAllOrders endpoint called for user: {UserId}", userId);

        var result = await orderService.GetAllOrder(userId);

        if (result.IsSuccessful)
        {
            Logger.Info("Orders retrieved successfully for user: {UserId}", userId);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("GetAllOrders failed for user: {UserId}, Error: {Error}", userId, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpGet("OrderId/{orderId}")]
    public async Task<IActionResult> GetById(Guid orderId)
    {
        Logger.Info("GetOrderById endpoint called for order: {OrderId}", orderId);

        var result = await orderService.GetByIdOrder(orderId);

        if (result.IsSuccessful)
        {
            Logger.Info("Order retrieved successfully for order: {OrderId}", orderId);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("GetOrderById failed for order: {OrderId}, Error: {Error}", orderId, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpDelete("{orderId}")]
    public async Task<IActionResult> Delete(Guid orderId)
    {
        Logger.Info("DeleteOrder endpoint called for order: {OrderId}", orderId);

        var result = await orderService.OrderDelete(orderId);

        if (result.IsSuccessful)
        {
            Logger.Info("Order deleted successfully for order: {OrderId}", orderId);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("DeleteOrder failed for order: {OrderId}, Error: {Error}", orderId, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }
}
