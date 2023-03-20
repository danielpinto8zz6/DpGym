using DpGym.Contracts.Dtos;

namespace DpGym.Application.Services;

public interface IAuthService
{
    Task<string> AuthenticateAsync(UserLoginDto loginDto);
}