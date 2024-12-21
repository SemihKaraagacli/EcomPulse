using EcomPulse.Repository.Entities;
using EcomPulse.Service.CreditCardService.Dtos;
using EcomPulse.Service.UserService.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EcomPulse.Service.UserService
{
    public class UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : IUserService
    {
        public async Task<ServiceResult> SignUp(UserCreateRequest request)
        {
            var hasUser = await userManager.FindByEmailAsync(request.Email);
            if (hasUser != null)
            {
                return ServiceResult.Fail("User already exists.", HttpStatusCode.BadRequest);
            }
            var newUser = new AppUser
            {
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
            };
            var user = await userManager.CreateAsync(newUser, request.Password);
            if (!user.Succeeded)
            {
                var errorList = user.Errors.Select(x => x.Description).ToList();
                return ServiceResult.Fail(errorList, HttpStatusCode.BadRequest);
            }
            return ServiceResult.Success(HttpStatusCode.OK);
        }
        public async Task<ServiceResult<IEnumerable<AllUserResponse>>> GetAllUser()
        {
            var allUser = await userManager.Users.Include(x => x.CreditCards).ToListAsync();
            var allUserResponses = new List<AllUserResponse>();

            foreach (var user in allUser)
            {
                var roles = await userManager.GetRolesAsync(user);
                var allUerResponse = new AllUserResponse(
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.PhoneNumber,
                    roles.ToList()
                );

                allUserResponses.Add(allUerResponse);
            }
            return ServiceResult<IEnumerable<AllUserResponse>>.Success(allUserResponses, HttpStatusCode.OK);
        }
        public async Task<ServiceResult> UserUpdate(UserUpdateRequest request)
        {
            var hasUser = await userManager.FindByIdAsync(request.Id.ToString());
            if (hasUser is null)
            {
                return ServiceResult.Fail("User already exists.", HttpStatusCode.NotFound);
            }
            hasUser.UserName = request.UserName;
            hasUser.Email = request.Email;
            hasUser.PhoneNumber = request.PhoneNumber;
            await userManager.UpdateAsync(hasUser);
            return ServiceResult.Success(HttpStatusCode.OK);
        }
        public async Task<ServiceResult<UserResponse>> GetByIdUser(Guid id)
        {
            var hasUser = await userManager.Users.Include(x => x.CreditCards).FirstOrDefaultAsync(x => x.Id == id);
            if (hasUser is null)
            {
                return ServiceResult<UserResponse>.Fail("User already exists.", HttpStatusCode.NotFound);
            }
            var hasRole = await userManager.GetRolesAsync(hasUser);
            var creditCards = hasUser.CreditCards.Select(x => new CreditCardResponse(
                x.UserId,
                x.CardHolderName,
                x.CardNumber,
                x.ExpirationDateFormatted,
                x.CVV,
                x.AvailableBalance
                )).ToList(); //Get CreditCard
            var userResponse = new UserResponse(
                hasUser.Id,
                hasUser.UserName,
                hasUser.Email,
                hasUser.PhoneNumber,
                hasRole.ToList(),
                creditCards);

            return ServiceResult<UserResponse>.Success(userResponse, HttpStatusCode.OK);
        }
        public async Task<ServiceResult> DeleteUser(Guid id)
        {
            var hasUser = await userManager.FindByIdAsync(id.ToString());
            if (hasUser is null)
            {
                return ServiceResult.Fail("User already exists.", HttpStatusCode.NotFound);
            }
            await userManager.DeleteAsync(hasUser);
            return ServiceResult.Success(HttpStatusCode.OK);
        }
    }
}
