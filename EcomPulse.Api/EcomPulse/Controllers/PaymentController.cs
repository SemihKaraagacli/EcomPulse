﻿using EcomPulse.Service.PaymentService;
using EcomPulse.Service.PaymentService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    [Authorize(AuthenticationSchemes = "SigninToken")]
    public class PaymentController(IPaymentService paymentService, ILogger<CustomControllerBase> logger) : CustomControllerBase(logger)
    {
        [HttpPost]
        public async Task<IActionResult> PaymentProcess(PaymentCreateRequest request)
        {
            var result = await paymentService.PaymnetProcessing(request);
            return CreateObjectResult(result);
        }
    }
}
