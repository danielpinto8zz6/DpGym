using AutoMapper;
using DpGym.Contracts.Models;
using DpGym.Domain;
using DpGym.Infrastructure.Repositories;

namespace DpGym.Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public Task CreateAsync(User user)
    {
        var userDomain = _mapper.Map<User, ApplicationUser>(user);

        return _userRepository.CreateAsync(userDomain);
    }

    public async Task<User> GetByIdAsync(string id)
    {
        var userDomain = await _userRepository.GetByIdAsync(id);

        return _mapper.Map<ApplicationUser, User>(userDomain);
    }

    public async Task DeleteByIdAsync(string id)
    {
        var userDomain = await _userRepository.GetByIdAsync(id) ;
        
        await _userRepository.DeleteAsync(userDomain);
    }

    public Task UpdateAsync(User user)
    {
        var userDomain = _mapper.Map<User, ApplicationUser>(user);

        return _userRepository.UpdateAsync(userDomain);
    }
}