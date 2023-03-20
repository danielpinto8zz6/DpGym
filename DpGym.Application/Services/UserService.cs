using AutoMapper;
using DpGym.Contracts.Dtos;
using DpGym.Domain;
using DpGym.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DpGym.Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(IMapper mapper, IUserRepository userRepository, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task<IdentityResult> RegisterAsync(UserRegistrationDto user)
    {
        var userDomain = _mapper.Map<UserRegistrationDto, ApplicationUser>(user);

        return await _userManager.CreateAsync(userDomain, user.Password);
    }
    
    public async Task<UserDto> GetByIdAsync(string id)
    {
        var userDomain = await _userRepository.GetByIdAsync(id);

        return _mapper.Map<ApplicationUser, UserDto>(userDomain);
    }

    public async Task DeleteByIdAsync(string id)
    {
        var userDomain = await _userRepository.GetByIdAsync(id);

        await _userRepository.DeleteAsync(userDomain);
    }

    public Task UpdateAsync(UserDto userDto)
    {
        var userDomain = _mapper.Map<UserDto, ApplicationUser>(userDto);

        return _userRepository.UpdateAsync(userDomain);
    }
    
    
}