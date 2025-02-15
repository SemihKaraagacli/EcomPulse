using BusinessLogicLayer.AuthService;
using BusinessLogicLayer.AuthService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

public class AuthController(IAuthService authService, ILogger<CustomControllerBase> logger) : CustomControllerBase(logger)
{
    [HttpPost]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> SignIn(SignInRequest request)
    {
        var result = await authService.SignIn(request);
        return CreateObjectResult(result);
    }
    [HttpPost("clientcredential")]
    public async Task<IActionResult> ClientCredential(ClientCredentialRequest request)
    {
        var result = await authService.ClientCredential(request);
        return CreateObjectResult(result);
    }
}
