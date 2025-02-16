using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DocsNetAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public IdentityController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }   

    [HttpPost]
    public IActionResult SignIn(User user)   
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Register(User user)   
    {
        return Ok();
    }
}
