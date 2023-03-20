using AutoMapper;
using DpGym.Contracts.Dtos;
using DpGym.Domain;

namespace DpGym.Mapper.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserDto>();
        CreateMap<UserRegistrationDto, ApplicationUser>();
    }
}