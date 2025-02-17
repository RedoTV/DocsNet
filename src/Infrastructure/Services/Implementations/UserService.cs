using Infrastructure.Services.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Services.Implementations;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public UserService(
        IMapper mapper,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration
        )
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
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

        SignInResult? signInResult = await _signInManager.PasswordSignInAsync(user, userDto.Password, false, lockoutOnFailure: true);
        if (!signInResult.Succeeded)
            return null;

        return GenerateToken(user.Id, user.UserName);
    }

    private string? GenerateToken(string userId, string? userName)
    {
        if (userName is null)
            return null;

        var key = _configuration.GetSection("Jwt")["Key"];
        var issuer = _configuration.GetSection("Jwt")["Issuer"];
        var audience = _configuration.GetSection("Jwt")["Audience"];
        var expirationDays = int.Parse(_configuration.GetSection("Jwt")["ExpirationDays"]!);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, userName)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(expirationDays),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
