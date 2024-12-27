using EcomPulse.Service.CreditCardService;
using EcomPulse.Service.CreditCardService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    [Authorize(AuthenticationSchemes = "SigninToken")]
    public class CreditCardController(ICreditCardService creditCardService, ILogger<CustomControllerBase> logger) : CustomControllerBase(logger)
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreditCardCreateRequest request)
        {
            var result = await creditCardService.CreateAsync(request);
            return CreateObjectResult(result);
        }
        [HttpPut]
        public async Task<IActionResult> Create(CreditCardUpdateRequest request)
        {
            var result = await creditCardService.UpdateAsync(request);
            return CreateObjectResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await creditCardService.DeleteAsync(id);
            return CreateObjectResult(result);
        }
    }
}
