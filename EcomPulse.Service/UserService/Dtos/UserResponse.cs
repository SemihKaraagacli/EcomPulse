using EcomPulse.Repository.Entities;
using EcomPulse.Service.CreditCardService.Dtos;
using EcomPulse.Service.RoleService.Dtos;

namespace EcomPulse.Service.UserService.Dtos
{
    public record UserResponse(Guid Id, string UserName, string Email, string PhoneNumber, List<string> Roles, List<CreditCardResponse> CreditCardResponse);
}
