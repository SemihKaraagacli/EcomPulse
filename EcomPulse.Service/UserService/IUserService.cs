using EcomPulse.Service.UserService.Dtos;

namespace EcomPulse.Service.UserService
{
    public interface IUserService
    {
        Task<ServiceResult<IEnumerable<UserResponse>>> GetAllUser();
        Task<ServiceResult> SignUp(UserCreateRequest request);
        Task<ServiceResult> UserUpdate(UserUpdateRequest request);
        Task<ServiceResult<UserResponse>> GetByIdUser(Guid id);
    }
}