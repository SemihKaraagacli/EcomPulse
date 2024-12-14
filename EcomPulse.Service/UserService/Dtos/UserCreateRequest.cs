using System.ComponentModel.DataAnnotations;

namespace EcomPulse.Service.UserService.Dtos
{
    public record UserCreateRequest(string UserName, string Email, string Password, string PhoneNumber);
}
