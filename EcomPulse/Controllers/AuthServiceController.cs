using EcomPulse.Service.AuthService;
using EcomPulse.Service.AuthService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    public class AuthServiceController(IAuthService authService) : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var result = await authService.SignIn(request);
            return CreateObjectResult(result);
        }
    }
}
