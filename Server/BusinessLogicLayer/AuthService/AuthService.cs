using BusinessLogicLayer.AuthService.Dtos;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace BusinessLogicLayer.AuthService;

public class AuthService(UserManager<AppUser> userManager, IConfiguration configuration) : IAuthService
{
    public async Task<Result<TokenResponse>> SignIn(SignInRequest request)
    {
        var hasUser = await userManager.FindByEmailAsync(request.Email);
        if (hasUser is null)
        {
            return Result<TokenResponse>.Failure(HttpStatusCode.BadRequest, "Email or password is wrong.");
        }

        var result = await userManager.CheckPasswordAsync(hasUser, request.Password);
        if (!result)
        {
            return Result<TokenResponse>.Failure(HttpStatusCode.BadRequest, "Email or password is wrong.");
        }


        var userClaims = new List<Claim>(); //create a claim list to hold information in the token and give it to the token.
        userClaims.Add(new Claim("id", hasUser.Id.ToString()));
        userClaims.Add(new Claim("username", hasUser.UserName!));
        userClaims.Add(new Claim("email", hasUser.Email!));
        userClaims.Add(new Claim("address", hasUser.Address));
        userClaims.Add(new Claim("city", hasUser.City));
        userClaims.Add(new Claim("county", hasUser.County));

        var hasRole = await userManager.GetRolesAsync(hasUser);
        foreach (var role in hasRole)
        {
            userClaims.Add(new Claim("role", role));
        }


        JwtSecurityToken newToken = new JwtSecurityToken(
            issuer: configuration.GetSection("SignIn_Token").GetValue<string>("Issuer"),
            claims: userClaims,
            expires: request.RememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddMinutes(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                      configuration.GetSection("SignIn_Token").GetValue<string>("SecretKey")!
                        )
                    ), SecurityAlgorithms.HmacSha256)
            );

        var accessTokenAsString = new JwtSecurityTokenHandler().WriteToken(newToken); // converts the resulting jwt token to string format
        return Result<TokenResponse>.Successful(new TokenResponse(accessTokenAsString));
    }
    public Task<Result<TokenResponse>> ClientCredential(ClientCredentialRequest request)
    {
        var clientId = configuration.GetSection("Client_Token")["ClientId"];
        var clientSecretKey = configuration.GetSection("Client_Token")["ClientSecretKey"];

        if (request.ClientId != clientId || request.ClientSecretKey != clientSecretKey)
        {
            return Task.FromResult(Result<TokenResponse>.Failure(HttpStatusCode.BadRequest, "ClientId or ClientSecretKey is wrong."));
        }

        var clientClaims = new List<Claim>();
        clientClaims.Add(new Claim("id", clientId));
        clientClaims.Add(new Claim("token_id", Guid.NewGuid().ToString()));

        JwtSecurityToken clientToken = new JwtSecurityToken(
            issuer: configuration.GetSection("SignIn_Token")["Issuer"],
            claims: clientClaims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Client_Token")["ClientSecretKey"]!)), SecurityAlgorithms.HmacSha256)
            );
        var accessTokenAsString = new JwtSecurityTokenHandler().WriteToken(clientToken);
        return Task.FromResult(Result<TokenResponse>.Successful(new TokenResponse(accessTokenAsString)));
    }
}
