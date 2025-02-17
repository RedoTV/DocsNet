using System.Security.Claims;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocsNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class DocumentController : ControllerBase
{
    private readonly ILogger<DocumentController> _logger;

    public DocumentController(ILogger<DocumentController> logger, DocsNetDbContext dbContext)
    {
        _logger = logger;
    }

    [HttpPost("AddDocument")]
    public async Task<Document> AddDocument()
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        throw new NotImplementedException();
    }

}
