namespace EcomPulse.Service.UserService.Dtos
{
    public record UserUpdateRequest(Guid Id, string UserName, string Email, string Password, string PhoneNumber);
}
