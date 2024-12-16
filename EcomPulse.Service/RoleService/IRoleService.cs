using EcomPulse.Service.RoleService.Dtos;

namespace EcomPulse.Service.RoleService
{
    public interface IRoleService
    {
        Task<ServiceResult> CreateRole(RoleCreateRequest request);
    }
}