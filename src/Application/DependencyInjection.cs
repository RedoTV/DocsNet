using Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services
    )
    {
        services.AddAutoMapper(typeof(UserProfile));

        return services;
    }
}
