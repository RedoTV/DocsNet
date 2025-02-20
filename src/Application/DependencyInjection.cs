using Application.Services.Implementations;
using Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var fileStoragePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            configuration["FileStoragePath"] ?? "wwwroot/documents"
        );

        services.AddScoped<IFileService>(provider =>
            new FileService(fileStoragePath));
        services.AddScoped<IDocumentService, DocumentService>();

        return services;
    }
}
