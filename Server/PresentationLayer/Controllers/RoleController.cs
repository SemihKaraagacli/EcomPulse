using BusinessLogicLayer.RoleService;
using BusinessLogicLayer.RoleService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class RoleController(IRoleService roleService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(RoleCreateRequest request)
    {
        var result = await roleService.CreateRole(request);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await roleService.GetAllRole();
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpPut]
    public async Task<IActionResult> Update(RoleUpdateRequest request)
    {
        var result = await roleService.UpdateRole(request);
        return result.IsSuccessful
             ? Ok(result)
             : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpDelete("{roleId}")]
    public async Task<IActionResult> Delete(Guid roleId)
    {
        var result = await roleService.DeleteRole(roleId);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpPost("{userId}/{roleId}")]
    public async Task<IActionResult> AddRoleToUser(Guid userId, Guid roleId)
    {
        var result = await roleService.AddRoleToUser(userId, roleId);
        return result.IsSuccessful
              ? Ok(result)
              : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpDelete("{userId}/{roleId}")]
    public async Task<IActionResult> RemoveRoleToUser(Guid userId, Guid roleId)
    {
        var result = await roleService.RemoveRoleToUser(userId, roleId);
        return result.IsSuccessful
             ? Ok(result)
             : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }

}
