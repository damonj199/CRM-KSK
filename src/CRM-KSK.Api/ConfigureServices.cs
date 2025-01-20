using CRM_KSK.Application.Interfaces;
using CRM_KSK.Application.Services;
using CRM_KSK.Dal.PostgreSQL;
using CRM_KSK.Dal.PostgreSQL.Repositories;
using CRM_KSK.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Api;

public static class ConfigureServices
{
    public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                builder => builder
                    .WithOrigins("http://localhost:7028")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(MapperProfile));

        services.AddDbContext<CRM_KSKDbContext>(
            options => options
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            .UseCamelCaseNamingConvention());

        services.AddScoped<IAdminRepository, AdminRepositiry>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
    }
}
