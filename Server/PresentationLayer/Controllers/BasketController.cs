using BusinessLogicLayer.BasketService;
using BusinessLogicLayer.BasketService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BasketController(IBasketService basketService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(BasketCreateRequest request)
    {
        var result = await basketService.CreateBasket(request);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(Guid userId)
    {
        var result = await basketService.GetBasket(userId);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(Guid userId)
    {
        var result = await basketService.DeleteBasket(userId);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
}
