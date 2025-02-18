using BusinessLogicLayer.BasketService;
using BusinessLogicLayer.BasketService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BasketController(IBasketService basketService) : ControllerBase
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    [HttpPost]
    public async Task<IActionResult> Create(BasketCreateRequest request)
    {
        Logger.Info("CreateBasket endpoint called with data: {Data}", request);

        var result = await basketService.CreateBasket(request);

        if (result.IsSuccessful)
        {
            Logger.Info("Basket created successfully for user: {UserId}", request.UserId);
            return Ok(result);
        }
        else
        {
            Logger.Warn("CreateBasket failed for user: {UserId}, Error: {Error}", request.UserId, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(Guid userId)
    {
        Logger.Info("GetBasket endpoint called for user: {UserId}", userId);

        var result = await basketService.GetBasket(userId);

        if (result.IsSuccessful)
        {
            Logger.Info("Basket retrieved successfully for user: {UserId}", userId);
            return Ok(result);
        }
        else
        {
            Logger.Warn("GetBasket failed for user: {UserId}, Error: {Error}", userId, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(Guid userId)
    {
        Logger.Info("DeleteBasket endpoint called for user: {UserId}", userId);

        var result = await basketService.DeleteBasket(userId);

        if (result.IsSuccessful)
        {
            Logger.Info("Basket deleted successfully for user: {UserId}", userId);
            return Ok(result);
        }
        else
        {
            Logger.Warn("DeleteBasket failed for user: {UserId}, Error: {Error}", userId, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }
}
