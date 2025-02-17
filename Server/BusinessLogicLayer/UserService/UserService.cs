using BusinessLogicLayer.CreditCardService.Dtos;
using BusinessLogicLayer.UserService.Dtos;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLogicLayer.UserService;

public class UserService(UserManager<AppUser> userManager) : IUserService
{
    public async Task<Result<string>> SignUp(UserCreateRequest request)
    {
        var hasUser = await userManager.FindByEmailAsync(request.Email);
        if (hasUser != null)
        {
            return Result<string>.Failure(HttpStatusCode.BadRequest, "User already exists.");
        }
        var newUser = new AppUser
        {
            Email = request.Email,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            City = request.City,
            County = request.County
        };
        var user = await userManager.CreateAsync(newUser, request.Password);
        if (!user.Succeeded)
        {
            var errorList = user.Errors.Select(x => x.Description).ToList();
            return Result<string>.Failure(HttpStatusCode.BadRequest, errorList);
        }
        return "Kullanıcı başarıyla oluşturuldu";
    }
    public async Task<Result<IEnumerable<AllUserResponse>>> GetAllUser()
    {
        var allUser = await userManager.Users.Include(x => x.CreditCards).ToListAsync();
        var allUserResponses = new List<AllUserResponse>();

        foreach (var user in allUser)
        {
            var roles = await userManager.GetRolesAsync(user);
            var allUerResponse = new AllUserResponse(
                user.Id,
                user.UserName!,
                user.Email!,
                user.PhoneNumber!,
                user.Address,
                user.City,
                user.County,
                roles.ToList()
            );

            allUserResponses.Add(allUerResponse);
        }
        return allUserResponses;
    }
    public async Task<Result<string>> UserUpdate(UserUpdateRequest request)
    {
        var hasUser = await userManager.FindByIdAsync(request.Id.ToString());
        if (hasUser is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "User already exists.");
        }
        hasUser.UserName = request.UserName;
        hasUser.Email = request.Email;
        hasUser.PhoneNumber = request.PhoneNumber;
        await userManager.UpdateAsync(hasUser);
        return "Kullanıcı başarıyla güncellendi";
    }
    public async Task<Result<UserResponse>> GetByIdUser(Guid id)
    {
        var hasUser = await userManager.Users.Include(x => x.CreditCards).FirstOrDefaultAsync(x => x.Id == id);
        if (hasUser is null)
        {
            return Result<UserResponse>.Failure(HttpStatusCode.NotFound, "User already exists.");
        }
        var hasRole = await userManager.GetRolesAsync(hasUser);
        var creditCards = hasUser.CreditCards.Select(x => new CreditCardResponse(
            x.Id,
            x.UserId,
            x.CardHolderName,
            x.CardNumber,
            x.ExpirationDateFormatted,
            x.CVV,
            x.AvailableBalance
            )).ToList(); //Get CreditCard
        var userResponse = new UserResponse(
            hasUser.Id,
            hasUser.UserName!,
            hasUser.Email!,
            hasUser.PhoneNumber!,
            hasUser.County,
            hasUser.City,
            hasUser.Address,
            hasRole.ToList(),
            creditCards);

        return userResponse;
    }
    public async Task<Result<string>> DeleteUser(Guid id)
    {
        var hasUser = await userManager.FindByIdAsync(id.ToString());
        if (hasUser is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "User already exists.");
        }
        await userManager.DeleteAsync(hasUser);
        return "Kullanıcı başarıyla silindi";
    }
}
