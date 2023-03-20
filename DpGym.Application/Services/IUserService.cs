using DpGym.Contracts.Dtos;
using Microsoft.AspNetCore.Identity;

namespace DpGym.Application.Services;

public interface IUserService
{
    Task<IdentityResult> RegisterAsync(UserRegistrationDto user);
    Task<UserDto> GetByIdAsync(string id);
    Task DeleteByIdAsync(string id);
    Task UpdateAsync(UserDto userDto);
}