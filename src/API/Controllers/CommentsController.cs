using System.Security.Claims;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Dtos.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocsNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;
    private readonly IMapper _mapper;

    public CommentsController(ICommentsService commentsService, IMapper mapper)
    {
        _commentsService = commentsService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(
        [FromBody] AddCommentRequestDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (request == null || string.IsNullOrEmpty(request.Text))
            {
                return BadRequest("Invalid request data.");
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            await _commentsService.AddCommentAsync(request.DocumentId, userId, request.Text, cancellationToken);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

        return Ok(new { message = "Comment added successfully." });
    }

    [HttpGet("{documentId}")]
    public async Task<IActionResult> GetComments(
        int documentId,
        CancellationToken cancellationToken)
    {
        try
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var comments = await _commentsService.GetCommentsByDocumentIdAsync(documentId, userId, cancellationToken);
            var commentsDto = new List<CommentResponseDto>();
            foreach (var comment in comments)
            {
                commentsDto.Add(_mapper.Map<CommentResponseDto>(comment));
            }

            return Ok(commentsDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
