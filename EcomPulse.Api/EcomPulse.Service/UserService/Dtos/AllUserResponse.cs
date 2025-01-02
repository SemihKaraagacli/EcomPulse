namespace EcomPulse.Service.UserService.Dtos
{
    public record AllUserResponse(Guid Id, string UserName, string Email, string PhoneNumber, string Address, string City, string County, List<string> Roles);
}
