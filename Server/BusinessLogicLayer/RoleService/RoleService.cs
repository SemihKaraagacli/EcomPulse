using BusinessLogicLayer.RoleService.Dtos;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLogicLayer.RoleService;

public class RoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager) : IRoleService
{
    public async Task<Result<string>> CreateRole(RoleCreateRequest request)
    {
        var hasRole = await roleManager.FindByNameAsync(request.RoleName);
        if (hasRole != null)
        {
            return Result<string>.Failure(HttpStatusCode.BadRequest, "Already exits role");
        }
        var roleResult = await roleManager.CreateAsync(new AppRole { Name = request.RoleName });
        if (!roleResult.Succeeded)
        {
            var errorList = roleResult.Errors.Select(x => x.Description).ToList();
            return Result<string>.Failure(HttpStatusCode.BadRequest, errorList);
        }
        return "Rol başarıyla oluşturuldu";
    }
    public async Task<Result<IEnumerable<RoleResponse>>> GetAllRole()
    {
        var allRole = await roleManager.Roles.ToListAsync();
        var roleResponse = allRole.Select(x => new RoleResponse(x.Id, x.Name!)).ToList();
        return roleResponse;
    }
    public async Task<Result<string>> UpdateRole(RoleUpdateRequest request)
    {
        var hasRole = await roleManager.FindByIdAsync(request.Id.ToString());
        if (hasRole is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Role not found");
        }
        hasRole.Name = request.RoleName;
        var roleResult = await roleManager.UpdateAsync(hasRole);
        return "Rol başarıyla güncellendi";
    }
    public async Task<Result<string>> DeleteRole(Guid roleId)
    {
        var hasRole = await roleManager.FindByIdAsync(roleId.ToString());
        if (hasRole is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Role not found");
        }
        await roleManager.DeleteAsync(hasRole);
        return "Rol başarıyla silindi";
    }
    public async Task<Result<string>> AddRoleToUser(Guid userId, Guid roleId)
    {
        var hasUser = await userManager.FindByIdAsync(userId.ToString());
        if (hasUser is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Role not found");
        }
        var hasRole = await roleManager.FindByIdAsync(roleId.ToString());
        if (hasRole is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Role not found");
        }
        var result = await userManager.AddToRoleAsync(hasUser, hasRole.Name!.ToString());
        if (!result.Succeeded)
        {
            var errorList = result.Errors.Select(x => x.Description).ToList();
            return Result<string>.Failure(HttpStatusCode.BadRequest, errorList);
        }
        return "Rol kullanıya başarıyla eklendi";
    }
    public async Task<Result<string>> RemoveRoleToUser(Guid userId, Guid roleId)
    {

        var hasUser = await userManager.FindByIdAsync(userId.ToString());
        if (hasUser is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "User not found");
        }
        var hasRole = await roleManager.FindByIdAsync(roleId.ToString());
        if (hasRole is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Role not found.");
        }
        var result = await userManager.RemoveFromRoleAsync(hasUser, hasRole.Name!.ToString());
        if (!result.Succeeded)
        {
            var errorList = result.Errors.Select(x => x.Description).ToList();
            return Result<string>.Failure(HttpStatusCode.BadRequest, errorList);
        }
        return "Rol kullanıcdan başarıyla silindi";
    }
}
