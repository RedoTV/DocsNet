using AutoMapper;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Implementations;

public class DocumentExpirationService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<DocumentExpirationService> _logger;
    private readonly IMapper _mapper;

    public DocumentExpirationService(
        IServiceScopeFactory scopeFactory,
        ILogger<DocumentExpirationService> logger,
        IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _mapper = mapper;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var documentCheckTask = CheckDocumentsPeriodically(cancellationToken);
        var historyCheckTask = CheckHistoryPeriodically(cancellationToken);

        await Task.WhenAll(documentCheckTask, historyCheckTask);
    }

    private async Task CheckDocumentsPeriodically(CancellationToken cancellationToken)
    {
        var documentCheckInterval = TimeSpan.FromMinutes(5);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await CheckDocuments(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error checking documents: {Message}", ex.Message);
            }

            await Task.Delay(documentCheckInterval, cancellationToken);
        }
    }

    private async Task CheckHistoryPeriodically(CancellationToken cancellationToken)
    {
        var historyCheckInterval = TimeSpan.FromHours(24);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await CheckHistory(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error checking history: {Message}", ex.Message);
            }

            await Task.Delay(historyCheckInterval, cancellationToken);
        }
    }

    private async Task CheckDocuments(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DocsNetDbContext>();

        var expiredDocuments = await dbContext.Documents
            .Where(d => d.ExpirationDate <= DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        foreach (var document in expiredDocuments)
        {
            var historyEntry = _mapper.Map<DocumentHistory>(document);
            dbContext.DocumentHistory.Add(historyEntry);
            dbContext.Documents.Remove(document);
        }

        if (expiredDocuments.Count() > 0)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task CheckHistory(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DocsNetDbContext>();

        var expiredHistory = await dbContext.DocumentHistory
            .Where(dh => dh.ExpirationDate <= DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        dbContext.DocumentHistory.RemoveRange(expiredHistory);

        if (expiredHistory.Count() > 0)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
