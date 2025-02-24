using Application.Services.Interfaces;
using Domain.Entities;

namespace Application.Services.Implementations;

public class CommentsService : ICommentsService
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IDocumentService _documentService;

    public CommentsService(ICommentsRepository commentsRepository, IDocumentService documentService)
    {
        _commentsRepository = commentsRepository;
        _documentService = documentService;
    }

    public async Task AddCommentAsync(int documentId, string userId, string text, CancellationToken cancellationToken)
    {
        await CheckUserAccess(documentId, userId, cancellationToken);

        var comment = new DocumentComment
        {
            DocumentId = documentId,
            UserId = userId,
            Text = text
        };

        await _commentsRepository.AddCommentAsync(comment, cancellationToken);
    }

    public async Task<List<DocumentComment>> GetCommentsByDocumentIdAsync(int documentId, string userId, CancellationToken cancellationToken)
    {
        await CheckUserAccess(documentId, userId, cancellationToken);

        return await _commentsRepository.GetCommentsByDocumentIdAsync(documentId, cancellationToken);
    }

    private async Task CheckUserAccess(int documentId, string userId, CancellationToken cancellationToken)
    {
        var document = await _documentService.GetDocumentByIdAsync(documentId, cancellationToken);
        if (document is null)
            throw new ArgumentException("Document not found");

        if (document.UserId != userId)
            throw new ArgumentException("User does not have access");
    }
}