using BusinessLogicLayer.PaymentService;
using BusinessLogicLayer.PaymentService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PaymentController(IPaymentService paymentService, ILogger<CustomControllerBase> logger) : CustomControllerBase(logger)
{
    [HttpPost]
    public async Task<IActionResult> PaymentProcess(PaymentCreateRequest request)
    {
        var result = await paymentService.PaymnetProcessing(request);
        return CreateObjectResult(result);
    }
}
