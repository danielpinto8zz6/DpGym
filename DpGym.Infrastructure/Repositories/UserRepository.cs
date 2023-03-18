using DpGym.Domain;
using DpGym.Infrastructure.Data;
using DpGym.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DpGym.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser> CreateAsync(ApplicationUser user)
    {
       var result = await _dbContext.Users.AddAsync(user);

       await _dbContext.SaveChangesAsync();
       
       return result.Entity;
    }

    public async Task<ApplicationUser> GetByIdAsync(string id)
    {
        var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        
        return result ?? throw new NotFoundException($"User with id {id} not found");
    }

    public async Task DeleteAsync(ApplicationUser user)
    {
        _dbContext.Users.Remove(user);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
    {
        var result = _dbContext.Users.Update(user);

        await _dbContext.SaveChangesAsync();

        return result.Entity;
    }
}