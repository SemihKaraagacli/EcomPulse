using BusinessLogicLayer.UserService;
using BusinessLogicLayer.UserService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> SignUp(UserCreateRequest request)
    {
        var result = await userService.SignUp(request);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Get()
    {
        var result = await userService.GetAllUser();
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update(UserUpdateRequest request)
    {
        var result = await userService.UserUpdate(request);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await userService.GetByIdUser(id);
        return result.IsSuccessful
             ? Ok(result)
             : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await userService.DeleteUser(id);
        return result.IsSuccessful
             ? Ok(result)
             : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
}
