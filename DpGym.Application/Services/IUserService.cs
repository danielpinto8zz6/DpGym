using DpGym.Contracts.Models;

namespace DpGym.Application.Services;

public interface IUserService
{
    Task CreateAsync(User user);
    Task<User> GetByIdAsync(string id);
    Task DeleteByIdAsync(string id);
    Task UpdateAsync(User user);
}