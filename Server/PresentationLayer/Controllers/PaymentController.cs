using BusinessLogicLayer.PaymentService;
using BusinessLogicLayer.PaymentService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    [HttpPost]
    public async Task<IActionResult> PaymentProcess(PaymentCreateRequest request)
    {
        Logger.Info("PaymentProcess endpoint called with data: {Data}", request);

        var result = await paymentService.PaymnetProcessing(request);

        if (result.IsSuccessful)
        {
            Logger.Info("Payment processed successfully with data: {Data}", request);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("Payment processing failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }
}
