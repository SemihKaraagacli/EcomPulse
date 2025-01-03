﻿using EcomPulse.Service.AuthService;
using EcomPulse.Service.AuthService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace EcomPulse.Controllers
{
    public class AuthServiceController(IAuthService authService, ILogger<CustomControllerBase> logger) : CustomControllerBase(logger)
    {
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
}
