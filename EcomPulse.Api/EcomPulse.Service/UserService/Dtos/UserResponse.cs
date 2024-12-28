using EcomPulse.Service.CreditCardService.Dtos;

namespace EcomPulse.Service.UserService.Dtos
{
    public record UserResponse(Guid Id, string UserName, string Email, string PhoneNumber, string Address, string City, string County, List<string> Roles, List<CreditCardResponse> CreditCardResponse);
}
