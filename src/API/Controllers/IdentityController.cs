using Domain.Dtos;
using Infrastructure.Services.Interfaces;
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
    public async Task<IActionResult> SignIn(UserSignInDto user)
    {
        var token = await _userService.SingInAsync(user);
        if (token is null)
            return BadRequest("Sign in Failed");

        return Ok(new { jwt = token });
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegisterDto user)
    {
        var identityResult = await _userService.RegisterAsync(user);
        if (identityResult is null)
            return BadRequest(new { message = "Invalid user form data" });

        if (!identityResult.Succeeded)
            return BadRequest(new { message = "Registration failed" });

        return Ok(identityResult);
    }
}
