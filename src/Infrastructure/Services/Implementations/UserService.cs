using Infrastructure.Services.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using AutoMapper;

namespace Infrastructure.Services.Implementations;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<IdentityResult?> RegisterAsync(UserRegisterDto userDto)
    {
        if (userDto.Name is null || userDto.Password is null)
            return null;

        User user = _mapper.Map<User>(userDto);
        return await _userManager.CreateAsync(user, userDto.Password);
    }

    public async Task<string?> SingInAsync(UserSignInDto userDto)
    {
        if (userDto.Name is null || userDto.Password is null)
            return null;

        User? user = await _signInManager.UserManager.FindByNameAsync(userDto.Name);
        if (user is null)
            return null;


        throw new NotImplementedException();
    }
}
