using EcomPulse.Service.AuthService.Dtos;

namespace EcomPulse.Service.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResult<TokenResponse>> SignIn(SignInRequest request);
    }
}