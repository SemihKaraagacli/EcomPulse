namespace EcomPulse.Service.UserService.Dtos
{
    public record UserResponse(Guid Id, string UserName, string Email, string PhoneNumber);
}
