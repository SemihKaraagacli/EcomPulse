using BusinessLogicLayer.CreditCardService;
using BusinessLogicLayer.CreditCardService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CreditCardController(ICreditCardService creditCardService) : ControllerBase
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    [HttpPost]
    public async Task<IActionResult> Create(CreditCardCreateRequest request)
    {
        Logger.Info("CreateCreditCard endpoint called with data: {Data}", request);

        var result = await creditCardService.CreateAsync(request);

        if (result.IsSuccessful)
        {
            Logger.Info("Credit card created successfully with data: {Data}", request);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("CreateCreditCard failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(CreditCardUpdateRequest request)
    {
        Logger.Info("UpdateCreditCard endpoint called with data: {Data}", request);

        var result = await creditCardService.UpdateAsync(request);

        if (result.IsSuccessful)
        {
            Logger.Info("Credit card updated successfully with data: {Data}", request);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("UpdateCreditCard failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        Logger.Info("DeleteCreditCard endpoint called for card: {Id}", id);

        var result = await creditCardService.DeleteAsync(id);

        if (result.IsSuccessful)
        {
            Logger.Info("Credit card deleted successfully for card: {Id}", id);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("DeleteCreditCard failed for card: {Id}, Error: {Error}", id, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }
}
