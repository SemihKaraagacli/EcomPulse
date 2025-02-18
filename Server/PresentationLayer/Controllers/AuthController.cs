using BusinessLogicLayer.AuthService;
using BusinessLogicLayer.AuthService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    [HttpPost]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> SignIn(SignInRequest request)
    {
        Logger.Info("SignIn endpoint called with email: {Email}", request.Email);

        var result = await authService.SignIn(request);

        if (result.IsSuccessful)
        {
            Logger.Info("SignIn successful for email: {Email}", request.Email);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("SignIn failed for email: {Email}, Error: {Error}", request.Email, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }
    [HttpPost("clientcredential")]
    public async Task<IActionResult> ClientCredential(ClientCredentialRequest request)
    {
        Logger.Info("ClientCredential endpoint called");

        var result = await authService.ClientCredential(request);

        if (result.IsSuccessful)
        {
            Logger.Info("ClientCredential authentication successful");
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("ClientCredential authentication failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }
}
