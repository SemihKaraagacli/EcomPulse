using EcomPulse.Service.UserService;
using EcomPulse.Service.UserService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    public class UserController(IUserService userService, ILogger<CustomControllerBase> logger) : CustomControllerBase(logger)
    {
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SignUp(UserCreateRequest request)
        {
            var result = await userService.SignUp(request);
            return CreateObjectResult(result);
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "SigninToken")]
        public async Task<IActionResult> Get()
        {
            var result = await userService.GetAllUser();
            return CreateObjectResult(result);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "SigninToken")]
        public async Task<IActionResult> Update(UserUpdateRequest request)
        {
            var result = await userService.UserUpdate(request);
            return CreateObjectResult(result);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "SigninToken")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await userService.GetByIdUser(id);
            return CreateObjectResult(result);
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "SigninToken")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await userService.DeleteUser(id);
            return CreateObjectResult(result);
        }
    }
}
