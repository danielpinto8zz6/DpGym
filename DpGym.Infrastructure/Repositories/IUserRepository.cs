using DpGym.Domain;

namespace DpGym.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser> GetByIdAsync(string id);
    Task DeleteAsync(ApplicationUser user);
    Task<ApplicationUser> UpdateAsync(ApplicationUser user);
}
