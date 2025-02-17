using Domain.Dtos;
using Infrastructure.Identity;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DocsNetAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IUserService _userService;

    public IdentityController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("SignIn")]
    public IActionResult SignIn(User user)
    {
        return Ok();
    }

    [HttpPost("Register")]
    public async Task<IdentityResult?> Register(UserRegisterDto user)
    {
        return await _userService.RegisterAsync(user);
    }
}
