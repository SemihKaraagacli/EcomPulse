using EcomPulse.Repository.Entities;
using EcomPulse.Service.RoleService.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EcomPulse.Service.RoleService
{
    public class RoleService(RoleManager<AppRole> roleManager) : IRoleService
    {
        public async Task<ServiceResult> CreateRole(RoleCreateRequest request)
        {
            var hasRole = await roleManager.FindByNameAsync(request.RoleName);
            if (hasRole != null)
            {
                return ServiceResult.Fail("Role already exists.", HttpStatusCode.BadRequest);
            }
            var roleResult = await roleManager.CreateAsync(new AppRole { Name = request.RoleName });
            if (!roleResult.Succeeded)
            {
                return ServiceResult.Fail("failed to create role.", HttpStatusCode.BadRequest);
            }
            return ServiceResult.Success(HttpStatusCode.OK);
        }
        public async Task<ServiceResult<IEnumerable<RoleResponse>>> GetAllRole()
        {
            var allRole = await roleManager.Roles.ToListAsync();
            var roleResponse = allRole.Select(x => new RoleResponse(x.Id, x.Name)).ToList();
            return ServiceResult<IEnumerable<RoleResponse>>.Success(roleResponse, HttpStatusCode.OK);
        }
        public async Task<ServiceResult> UpdateRole(RoleUpdateRequest request)
        {
            var hasRole = await roleManager.FindByIdAsync(request.Id.ToString());
            if (hasRole is null)
            {
                return ServiceResult.Fail("Role not found.", HttpStatusCode.NotFound);
            }
            hasRole.Name = request.RoleName;
            var roleResult = await roleManager.UpdateAsync(hasRole);
            return ServiceResult.Success(HttpStatusCode.OK);
        }
        public async Task<ServiceResult> DeleteRole(Guid roleId)
        {
            var hasRole = await roleManager.FindByIdAsync(roleId.ToString());
            if (hasRole is null)
            {
                return ServiceResult.Fail("Role not found.", HttpStatusCode.NotFound);
            }
            await roleManager.DeleteAsync(hasRole);
            return ServiceResult.Success(HttpStatusCode.OK);
        }
    }
}
