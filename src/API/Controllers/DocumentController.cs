using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocsNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class DocumentController : ControllerBase
{
    private readonly ILogger<DocumentController> _logger;

    public DocumentController(ILogger<DocumentController> logger)
    {
        _logger = logger;
    }

}
