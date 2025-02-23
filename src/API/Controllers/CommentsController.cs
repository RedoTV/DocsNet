using Application.Services.Interfaces;
using Domain.Dtos.Comment;
using Microsoft.AspNetCore.Mvc;

namespace DocsNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;

    public CommentsController(ICommentsService commentsService)
    {
        _commentsService = commentsService;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddComment(
        [FromBody] AddCommentRequest request,
        CancellationToken cancellationToken)
    {
        if (request == null || string.IsNullOrEmpty(request.Text))
        {
            return BadRequest("Invalid request data.");
        }

        await _commentsService.AddCommentAsync(request.DocumentId, request.UserId, request.Text, cancellationToken);

        return Ok("Comment added successfully.");
    }

    [HttpGet("Get/{documentId}")]
    public async Task<IActionResult> GetComments(
        int documentId,
        CancellationToken cancellationToken)
    {
        var comments = await _commentsService.GetCommentsByDocumentIdAsync(documentId, cancellationToken);

        return Ok(comments);
    }
}
