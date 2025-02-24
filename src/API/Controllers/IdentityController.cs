using Domain.Dtos;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocsNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
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
        try
        {
            var token = await _userService.SingInAsync(user);
            if (token is null)
                return BadRequest("Sign in Failed");

            return Ok(new { jwt = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegisterDto user)
    {
        try
        {
            var identityResult = await _userService.RegisterAsync(user);

            if (identityResult is null)
                return BadRequest(new { message = "Invalid user form data" });

            if (!identityResult.Succeeded)
                return BadRequest(new { message = "Registration failed" });

            return Ok(identityResult);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
