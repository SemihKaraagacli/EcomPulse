using BusinessLogicLayer.RoleService;
using BusinessLogicLayer.RoleService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class RoleController(IRoleService roleService, ILogger<CustomControllerBase> logger) : CustomControllerBase(logger)
{
    [HttpPost]
    public async Task<IActionResult> Create(RoleCreateRequest request)
    {
        var result = await roleService.CreateRole(request);
        return CreateObjectResult(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await roleService.GetAllRole();
        return CreateObjectResult(result);
    }
    [HttpPut]
    public async Task<IActionResult> Update(RoleUpdateRequest request)
    {
        var result = await roleService.UpdateRole(request);
        return CreateObjectResult(result);
    }
    [HttpDelete("{roleId}")]
    public async Task<IActionResult> Delete(Guid roleId)
    {
        var result = await roleService.DeleteRole(roleId);
        return CreateObjectResult(result);
    }
    [HttpPost("{userId}/{roleId}")]
    public async Task<IActionResult> AddRoleToUser(Guid userId, Guid roleId)
    {
        var result = await roleService.AddRoleToUser(userId, roleId);
        return CreateObjectResult(result);
    }
    [HttpDelete("{userId}/{roleId}")]
    public async Task<IActionResult> RemoveRoleToUser(Guid userId, Guid roleId)
    {
        var result = await roleService.RemoveRoleToUser(userId, roleId);
        return CreateObjectResult(result);
    }

}
