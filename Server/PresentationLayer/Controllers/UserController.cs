using BusinessLogicLayer.UserService;
using BusinessLogicLayer.UserService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    [HttpPost]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> SignUp(UserCreateRequest request)
    {
        Logger.Info("SignUp endpoint called with data: {Data}", request);

        var result = await userService.SignUp(request);

        if (result.IsSuccessful)
        {
            Logger.Info("User signed up successfully with data: {Data}", request);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("SignUp failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Get()
    {
        Logger.Info("GetAllUsers endpoint called");

        var result = await userService.GetAllUser();

        if (result.IsSuccessful)
        {
            Logger.Info("Users retrieved successfully");
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("GetAllUsers failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update(UserUpdateRequest request)
    {
        Logger.Info("UpdateUser endpoint called with data: {Data}", request);

        var result = await userService.UserUpdate(request);

        if (result.IsSuccessful)
        {
            Logger.Info("User updated successfully with data: {Data}", request);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("UpdateUser failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Get(Guid id)
    {
        Logger.Info("GetUserById endpoint called for user: {UserId}", id);

        var result = await userService.GetByIdUser(id);

        if (result.IsSuccessful)
        {
            Logger.Info("User retrieved successfully for user: {UserId}", id);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("GetUserById failed for user: {UserId}, Error: {Error}", id, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid id)
    {
        Logger.Info("DeleteUser endpoint called for user: {UserId}", id);

        var result = await userService.DeleteUser(id);

        if (result.IsSuccessful)
        {
            Logger.Info("User deleted successfully for user: {UserId}", id);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("DeleteUser failed for user: {UserId}, Error: {Error}", id, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }
}
