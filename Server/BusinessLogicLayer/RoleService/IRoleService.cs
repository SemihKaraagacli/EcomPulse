using BusinessLogicLayer.RoleService.Dtos;

namespace BusinessLogicLayer.RoleService;

public interface IRoleService
{
    Task<Result<string>> CreateRole(RoleCreateRequest request);
    Task<Result<IEnumerable<RoleResponse>>> GetAllRole();
    Task<Result<string>> UpdateRole(RoleUpdateRequest request);
    Task<Result<string>> DeleteRole(Guid roleId);
    Task<Result<string>> AddRoleToUser(Guid userId, Guid roleId);
    Task<Result<string>> RemoveRoleToUser(Guid userId, Guid roleId);
}