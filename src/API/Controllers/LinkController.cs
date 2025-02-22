using System.Security.Claims;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocsNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class LinkController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public LinkController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet("{documentId}")]
    public async Task<ActionResult> GetShareLink(int documentId, CancellationToken cancellationToken)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        try
        {
            string? shareLink = await _documentService.GetShareLinkAsync(userId, documentId, cancellationToken);
            if (shareLink == string.Empty)
                return BadRequest(new { message = "Share link not found by this userId" });

            return Ok(shareLink);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult> CopyDocumentByShareLink(string shareLink, DateTime? newExpirationDate, CancellationToken cancellationToken)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        try
        {
            var copiedDocument = await _documentService.CopyDocumentByShareLinkAsync(
                shareLink,
                userId,
                newExpirationDate,
                cancellationToken
            );

            return Ok(new
            {
                documentId = copiedDocument.Id,
                name = copiedDocument.Name,
                expirationDate = copiedDocument.ExpirationDate
            });
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
