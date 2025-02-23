using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Implementations;

public class CommentsRepository : ICommentsRepository
{
    private readonly DocsNetDbContext _docsNetDb;

    public CommentsRepository(DocsNetDbContext docsNetDb)
    {
        _docsNetDb = docsNetDb;
    }

    public async Task AddCommentAsync(DocumentComment comment, CancellationToken cancellationToken)
    {
        await _docsNetDb.DocumentComments.AddAsync(comment, cancellationToken);

        await _docsNetDb.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<DocumentComment>> GetCommentsByDocumentIdAsync(int documentId, CancellationToken cancellationToken)
    {
        return await _docsNetDb.DocumentComments
            .Where(c => c.DocumentId == documentId)
            .ToListAsync(cancellationToken);
    }
}
