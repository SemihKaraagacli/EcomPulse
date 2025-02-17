using BusinessLogicLayer.CreditCardService;
using BusinessLogicLayer.CreditCardService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CreditCardController(ICreditCardService creditCardService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreditCardCreateRequest request)
    {
        var result = await creditCardService.CreateAsync(request);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpPut]
    public async Task<IActionResult> Create(CreditCardUpdateRequest request)
    {
        var result = await creditCardService.UpdateAsync(request);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await creditCardService.DeleteAsync(id);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
}
