using BusinessLogicLayer.UserService.Dtos;

namespace BusinessLogicLayer.UserService;

public interface IUserService
{
    Task<Result<IEnumerable<AllUserResponse>>> GetAllUser();
    Task<Result<string>> SignUp(UserCreateRequest request);
    Task<Result<string>> UserUpdate(UserUpdateRequest request);
    Task<Result<UserResponse>> GetByIdUser(Guid id);
    Task<Result<string>> DeleteUser(Guid id);
}