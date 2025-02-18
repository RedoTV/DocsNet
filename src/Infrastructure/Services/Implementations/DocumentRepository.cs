using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Implementations;

public class DocumentRepository : IFileReadRepository<Document>, IFileWriteRepository<Document>
{
    private readonly DocsNetDbContext _docsNetDb;

    public DocumentRepository(DocsNetDbContext docsNetDb)
    {
        _docsNetDb = docsNetDb;
    }

    public async Task AddFileAsync(Document file, CancellationToken cancellationToken)
    {
        await _docsNetDb.Documents.AddAsync(file, cancellationToken);
    }

    public async Task DeleteFileAsync(Document file, CancellationToken cancellationToken)
    {
        await Task.Run(() => _docsNetDb.Documents.Remove(file), cancellationToken);
    }

    public async Task<Document?> GetFileAsync(int fileId, CancellationToken cancellationToken)
    {
        return await _docsNetDb.Documents
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == fileId, cancellationToken);
    }

    public async Task<IEnumerable<Document?>> GetUserFilesAsync(string userId, CancellationToken cancellationToken)
    {
        return await _docsNetDb.Documents
            .AsNoTracking()
            .Where(d => d.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _docsNetDb.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateFileAsync(Document file, CancellationToken cancellationToken)
    {
        return _docsNetDb.Documents
        .Where(d => d.Id == file.Id)
        .ExecuteUpdateAsync(s => s
            .SetProperty(d => d.Name, file.Name)
            .SetProperty(d => d.Description, file.Description)
            .SetProperty(d => d.FilePath, file.FilePath)
            .SetProperty(d => d.ShareLink, file.ShareLink)
            .SetProperty(d => d.ExpirationDate, file.ExpirationDate),
            cancellationToken);
    }
}
