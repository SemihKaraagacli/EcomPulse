namespace BusinessLogicLayer.AuthService.Dtos;

public record SignInRequest(
    string Email,
    string Password,
    bool RememberMe
    );
