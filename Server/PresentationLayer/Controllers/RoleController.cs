using BusinessLogicLayer.RoleService;
using BusinessLogicLayer.RoleService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class RoleController(IRoleService roleService) : ControllerBase
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    [HttpPost]
    public async Task<IActionResult> Create(RoleCreateRequest request)
    {
        Logger.Info("CreateRole endpoint called with data: {Data}", request);

        var result = await roleService.CreateRole(request);

        if (result.IsSuccessful)
        {
            Logger.Info("Role created successfully with data: {Data}", request);
            return Ok(result);
        }
        else
        {
            Logger.Warn("CreateRole failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Logger.Info("GetAllRoles endpoint called");

        var result = await roleService.GetAllRole();

        if (result.IsSuccessful)
        {
            Logger.Info("Roles retrieved successfully");
            return Ok(result);
        }
        else
        {
            Logger.Warn("GetAllRoles failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(RoleUpdateRequest request)
    {
        Logger.Info("UpdateRole endpoint called with data: {Data}", request);

        var result = await roleService.UpdateRole(request);

        if (result.IsSuccessful)
        {
            Logger.Info("Role updated successfully with data: {Data}", request);
            return Ok(result);
        }
        else
        {
            Logger.Warn("UpdateRole failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpDelete("{roleId}")]
    public async Task<IActionResult> Delete(Guid roleId)
    {
        Logger.Info("DeleteRole endpoint called for role: {RoleId}", roleId);

        var result = await roleService.DeleteRole(roleId);

        if (result.IsSuccessful)
        {
            Logger.Info("Role deleted successfully for role: {RoleId}", roleId);
            return Ok(result);
        }
        else
        {
            Logger.Warn("DeleteRole failed for role: {RoleId}, Error: {Error}", roleId, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpPost("{userId}/{roleId}")]
    public async Task<IActionResult> AddRoleToUser(Guid userId, Guid roleId)
    {
        Logger.Info("AddRoleToUser endpoint called for user: {UserId}, role: {RoleId}", userId, roleId);

        var result = await roleService.AddRoleToUser(userId, roleId);

        if (result.IsSuccessful)
        {
            Logger.Info("Role added to user successfully for user: {UserId}, role: {RoleId}", userId, roleId);
            return Ok(result);
        }
        else
        {
            Logger.Warn("AddRoleToUser failed for user: {UserId}, role: {RoleId}, Error: {Error}", userId, roleId, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpDelete("{userId}/{roleId}")]
    public async Task<IActionResult> RemoveRoleToUser(Guid userId, Guid roleId)
    {
        Logger.Info("RemoveRoleToUser endpoint called for user: {UserId}, role: {RoleId}", userId, roleId);

        var result = await roleService.RemoveRoleToUser(userId, roleId);

        if (result.IsSuccessful)
        {
            Logger.Info("Role removed from user successfully for user: {UserId}, role: {RoleId}", userId, roleId);
            return Ok(result);
        }
        else
        {
            Logger.Warn("RemoveRoleToUser failed for user: {UserId}, role: {RoleId}, Error: {Error}", userId, roleId, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

}
