using System.Security.Claims;
using Application.Services.Interfaces;
using DocsNetAPI.Dtos.Document;
using Domain.Dtos.Document;
using Domain.Dtos.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocsNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadDocument(DocumentUploadRequest documentUploadData)
    {
        if (documentUploadData.File is null || documentUploadData.File.Length == 0)
        {
            return BadRequest("File is not selected");
        }

        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        var documentDto = new DocumentUploadDto
        {
            DocumentName = documentUploadData.File.FileName,
            DocumentDescription = documentUploadData.DocumentDescription,
            ExpirationDate = documentUploadData.ExpirationDate,
            FileData = new FileData()
            {
                FileName = documentUploadData.File.FileName,
                FileStream = documentUploadData.File.OpenReadStream(),
                ContentType = documentUploadData.File.ContentType
            }
        };

        var uploadedDocument = await _documentService
            .UploadDocumentAsync(
                documentDto,
                userId,
                HttpContext.RequestAborted
            );

        return Ok(new { documentId = uploadedDocument.Id, filePath = uploadedDocument.FilePath });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDocument(int id)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        var document = await _documentService.GetDocumentByIdAsync(id, HttpContext.RequestAborted);

        if (document is null || document.UserId != userId)
        {
            return NotFound(new { message = "Document not found" });
        }

        return Ok(new { documentId = document.Id, name = document.Name, filePath = document.FilePath });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument(int id)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        bool isDeleted = await _documentService.RemoveDocumentAsync(id, userId, HttpContext.RequestAborted);

        if (!isDeleted)
        {
            return NotFound(new { message = "Document not found" });
        }

        return NoContent();
    }

    [HttpGet("download/{documentId}")]
    public async Task<IActionResult> DownloadDocument(int documentId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var document = await _documentService.GetDocumentByIdAsync(documentId, cancellationToken);

        if (document is null)
            return NotFound(new { message = "Document not found" });

        if (document.UserId != userId)
            return NotFound(new { message = "Access denied" });

        var fileStream = new FileStream(document.FilePath, FileMode.Open, FileAccess.Read);
        return File(fileStream, document.ContentType);
    }
}
