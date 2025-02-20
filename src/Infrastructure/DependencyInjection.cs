using System.Text;
using Application.Mapper;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Infrastructure.Services.Implementations;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddAutoMapper(typeof(DocumentProfile), typeof(UserProfile));

        services.AddScoped<IFileReadRepository<Document>, DocumentRepository>();
        services.AddScoped<IFileWriteRepository<Document>, DocumentRepository>();

        services.AddDbContext<DocsNetDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DbConnection"), b => b.MigrationsAssembly("DocsNetAPI")));

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<DocsNetDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });

        services.AddTransient<IUserService, UserService>();

        return services;
    }
}
