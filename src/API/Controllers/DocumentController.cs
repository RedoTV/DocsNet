using System.Security.Claims;
using Application.Services.Interfaces;
using AutoMapper;
using DocsNetAPI.Dtos.Document;
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
    private readonly IMapper _mapper;

    public DocumentController(IDocumentService documentService, IMapper mapper)
    {
        _documentService = documentService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> UploadDocument([FromForm] DocumentUploadRequest documentUploadData, CancellationToken cancellationToken)
    {
        try
        {
            if (documentUploadData.File is null || documentUploadData.File.Length == 0)
            {
                return BadRequest("File is not selected");
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            var documentDto = _mapper.Map<DocumentUploadDto>(documentUploadData);

            var uploadedDocument = await _documentService.UploadDocumentAsync(documentDto, userId, cancellationToken);

            return Ok(new { documentId = uploadedDocument.Id });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDocument(int id, CancellationToken cancellationToken)
    {
        try
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var document = await _documentService.GetDocumentByIdAsync(id, cancellationToken);

            if (document is null || document.UserId != userId)
            {
                return NotFound(new { message = "Document not found" });
            }

            return Ok(_mapper.Map<DocumentResponseDto>(document));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument(int id, CancellationToken cancellationToken)
    {
        try
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            bool isDeleted = await _documentService.RemoveDocumentAsync(id, userId, cancellationToken);

            if (!isDeleted)
            {
                return NotFound(new { message = "Document not found" });
            }

            return Ok(new { isDeleted = isDeleted });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDocument([FromForm] DocumentUpdateRequest updateRequest, CancellationToken cancellationToken)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        var documentDto = _mapper.Map<DocumentUpdateDto>(updateRequest);

        try
        {
            await _documentService.UpdateDocumentAsync(documentDto, userId, cancellationToken);
            return Ok(new { message = "Document updated successfully" });
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("download/{documentId}")]
    public async Task<IActionResult> DownloadDocument(int documentId, CancellationToken cancellationToken)
    {
        try
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
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
