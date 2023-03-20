using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DpGym.Contracts.Dtos;
using DpGym.Domain;
using DpGym.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AuthenticationException = System.Security.Authentication.AuthenticationException;

namespace DpGym.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> AuthenticateAsync(UserLoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);
        if (user is null)
        {
            throw new NotFoundException($"User {loginDto.UserName} not found!");
        }
        
        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result)
        {
            throw new AuthenticationException();
        }
        
        return await CreateTokenAsync(user);
    }

    private async Task<string> CreateTokenAsync(ApplicationUser user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtConfig = _configuration.GetSection("jwtConfig");
        var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }       

    private async Task<List<Claim>> GetClaims(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName)
        };
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtConfig");
        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
}