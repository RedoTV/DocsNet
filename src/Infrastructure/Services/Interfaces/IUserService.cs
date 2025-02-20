using Domain.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Interfaces;

public interface IUserService
{
    public Task<string?> SingInAsync(UserSignInDto userDto);
    public Task<IdentityResult?> RegisterAsync(UserRegisterDto userDto);
}
