using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocsNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentController : ControllerBase
{
    private readonly ILogger<DocumentController> _logger;
    private readonly DocsNetDbContext dbContext;

    public DocumentController(ILogger<DocumentController> logger, DocsNetDbContext dbContext)
    {
        _logger = logger;
        this.dbContext = dbContext;
    }

    [HttpPost("AddDocument")]
    public async Task<Document> AddDocument()
    {
        Document document = new Document() { Name = "File", Description = "New file", UserId = Guid.NewGuid().ToString() };
        dbContext.Documents.Add(document);

        await dbContext.SaveChangesAsync();
        return document;
    }

}
