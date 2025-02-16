using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Application.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserSignInDto, IUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(user => user.Name));
        CreateMap<UserRegisterDto, IUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(user => user.Name));
    }
}
