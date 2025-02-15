namespace BusinessLogicLayer.UserService.Dtos;

public record UserUpdateRequest(Guid Id, string UserName, string Email, string PhoneNumber, string Address, string City, string County);
