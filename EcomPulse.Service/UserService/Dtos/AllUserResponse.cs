using EcomPulse.Service.RoleService.Dtos;

namespace EcomPulse.Service.UserService.Dtos
{
    public record AllUserResponse(Guid Id, string UserName, string Email, string PhoneNumber, List<string> Roles);
}
