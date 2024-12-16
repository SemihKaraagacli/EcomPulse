using EcomPulse.Service.RoleService;
using EcomPulse.Service.RoleService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    public class RoleController(IRoleService roleService) : CustomControllerBase
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
    }
}
