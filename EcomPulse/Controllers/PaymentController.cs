using EcomPulse.Service.PaymentService;
using EcomPulse.Service.PaymentService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    public class PaymentController(IPaymentService paymentService) : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PaymentProcess(PaymentCreateRequest request)
        {
            var result = await paymentService.PaymnetProcessing(request);
            return CreateObjectResult(result);
        }
    }
}
