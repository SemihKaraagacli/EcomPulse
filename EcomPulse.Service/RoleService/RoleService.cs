using EcomPulse.Repository.Entities;
using EcomPulse.Service.RoleService.Dtos;
using Microsoft.AspNetCore.Identity;
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
    }
}
