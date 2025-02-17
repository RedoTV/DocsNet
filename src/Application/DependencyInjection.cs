using Application.Mapper;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services
    )
    {
        services.AddTransient<IDocumentService, DocumentService>();

        return services;
    }
}
