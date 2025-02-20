using System.Security.Claims;
using Application.Services.Interfaces;
using Domain.Dtos.Document;
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

    [HttpPost("UploadDocument")]
    public async Task<IActionResult> UploadDocument(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0)
        {
            return BadRequest("Файл не выбран.");
        }

        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        var documentDto = new DocumentUploadDto
        {
            DocumentName = formFile.FileName,
            Stream = formFile.OpenReadStream(),
            ContentType = formFile.ContentType
        };

        var uploadedDocument = await _documentService.UploadDocumentAsync(documentDto, userId, HttpContext.RequestAborted);

        return Ok(new { documentId = uploadedDocument.Id, filePath = uploadedDocument.FilePath });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDocument(int id)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        var document = await _documentService.GetDocumentByIdAsync(id, HttpContext.RequestAborted);

        if (document == null || document.UserId != userId)
        {
            return NotFound("Документ не найден или доступ запрещен.");
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
            return NotFound("Документ не найден или доступ запрещен.");
        }

        return NoContent();
    }
}
