using BusinessLogicLayer.AuthService.Dtos;

namespace BusinessLogicLayer.AuthService;

public interface IAuthService
{
    Task<Result<TokenResponse>> SignIn(SignInRequest request);
    Task<Result<TokenResponse>> ClientCredential(ClientCredentialRequest request);
}