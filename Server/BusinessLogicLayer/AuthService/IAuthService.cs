using BusinessLogicLayer.AuthService.Dtos;

namespace BusinessLogicLayer.AuthService;

public interface IAuthService
{
    Task<ServiceResult<TokenResponse>> SignIn(SignInRequest request);
    Task<ServiceResult<TokenResponse>> ClientCredential(ClientCredentialRequest request);
}