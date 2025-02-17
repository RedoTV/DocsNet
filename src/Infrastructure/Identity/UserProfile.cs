using AutoMapper;
using Domain.Dtos;

namespace Infrastructure.Identity;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserSignInDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(user => user.Name))
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

        CreateMap<UserRegisterDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(user => user.Name))
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
    }
}
