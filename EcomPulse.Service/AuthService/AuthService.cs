using EcomPulse.Repository.Entities;
using EcomPulse.Service.AuthService.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace EcomPulse.Service.AuthService
{
    public class AuthService(UserManager<AppUser> userManager, IConfiguration configuration) : IAuthService
    {
        public async Task<ServiceResult<TokenResponse>> SignIn(SignInRequest request)
        {
            var hasUser = await userManager.FindByEmailAsync(request.Email);
            if (hasUser is null)
            {
                return ServiceResult<TokenResponse>.Fail("Email or password is wrong.", HttpStatusCode.BadRequest);
            }

            var result = await userManager.CheckPasswordAsync(hasUser, request.Password);
            if (!result)
            {
                return ServiceResult<TokenResponse>.Fail("Email or password is wrong.", HttpStatusCode.BadRequest);
            }


            var userClaims = new List<Claim>(); //create a claim list to hold information in the token and give it to the token.
            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, hasUser.Id.ToString()));
            userClaims.Add(new Claim(ClaimTypes.Name, hasUser.UserName));
            userClaims.Add(new Claim(ClaimTypes.Email, hasUser.Email));
            userClaims.Add(new Claim("Address", hasUser.Address));
            userClaims.Add(new Claim("City", hasUser.City));
            userClaims.Add(new Claim("County", hasUser.County));

            var hasRole = await userManager.GetRolesAsync(hasUser);
            foreach (var role in hasRole)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }


            JwtSecurityToken newToken = new JwtSecurityToken(
                issuer: configuration.GetSection("TokenOptions").GetValue<string>("Issuer"),
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                          configuration.GetSection("TokenOptions").GetValue<string>("SecretKey")!
                            )
                        ), SecurityAlgorithms.HmacSha256)
                );

            var accessTokenAsString = new JwtSecurityTokenHandler().WriteToken(newToken); // converts the resulting jwt token to string format
            return ServiceResult<TokenResponse>.Success(new TokenResponse(accessTokenAsString), HttpStatusCode.OK);
        }
    }
}
