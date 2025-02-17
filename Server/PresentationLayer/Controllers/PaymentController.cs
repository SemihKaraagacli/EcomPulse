using BusinessLogicLayer.PaymentService;
using BusinessLogicLayer.PaymentService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PaymentProcess(PaymentCreateRequest request)
    {
        var result = await paymentService.PaymnetProcessing(request);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
}
