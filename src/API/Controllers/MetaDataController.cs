using System.Security.Claims;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Dtos.Metadata;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocsNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class MetaDataController : ControllerBase
{
    private readonly IMetadataService _metadataService;
    private readonly IMapper _mapper;

    public MetaDataController(IMetadataService metadataService, IMapper mapper)
    {
        _metadataService = metadataService;
        _mapper = mapper;
    }

    [HttpPost("{documentId}")]
    public async Task<IActionResult> SetMetadataToDocument(int documentId, [FromBody] AddMetadataRequestDto metadata, CancellationToken cancellationToken)
    {
        try
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            var documentMetadata = _mapper.Map<DocumentMetadata>(metadata);

            await _metadataService.SetMetadataAsync(documentId, userId, documentMetadata, cancellationToken);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

        return Ok(new { message = "Metadata set successfully." });
    }

    [HttpGet("{documentId}")]
    public async Task<IActionResult> GetDocumentMetadata(int documentId, CancellationToken cancellationToken)
    {
        try
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var documentMetadata = await _metadataService.GetMetadataAsync(documentId, userId, cancellationToken);
            return Ok(_mapper.Map<MetadataResponseDto>(documentMetadata));

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
