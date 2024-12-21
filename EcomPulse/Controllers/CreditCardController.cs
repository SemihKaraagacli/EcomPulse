﻿using EcomPulse.Service.CreditCardService;
using EcomPulse.Service.CreditCardService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    public class CreditCardController(ICreditCardService creditCardService) : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreditCardCreateRequest request)
        {
            var result = await creditCardService.CreateAsync(request);
            return CreateObjectResult(result);
        }
    }
}