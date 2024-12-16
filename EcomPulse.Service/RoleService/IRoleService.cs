using EcomPulse.Service.RoleService.Dtos;

namespace EcomPulse.Service.RoleService
{
    public interface IRoleService
    {
        Task<ServiceResult> CreateRole(RoleCreateRequest request);
        Task<ServiceResult<IEnumerable<RoleResponse>>> GetAllRole();
        Task<ServiceResult> UpdateRole(RoleUpdateRequest request);
        Task<ServiceResult> DeleteRole(Guid roleId);
    }
}