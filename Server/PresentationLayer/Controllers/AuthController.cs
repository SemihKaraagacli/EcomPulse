using BusinessLogicLayer.AuthService;
using BusinessLogicLayer.AuthService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> SignIn(SignInRequest request)
    {
        var result = await authService.SignIn(request);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpPost("clientcredential")]
    public async Task<IActionResult> ClientCredential(ClientCredentialRequest request)
    {
        var result = await authService.ClientCredential(request);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
}
