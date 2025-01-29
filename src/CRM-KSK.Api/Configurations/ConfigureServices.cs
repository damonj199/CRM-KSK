using CRM_KSK.Application.Interfaces;
using CRM_KSK.Application.Services;
using CRM_KSK.Dal.PostgreSQL;
using CRM_KSK.Dal.PostgreSQL.Repositories;
using CRM_KSK.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CRM_KSK.Api.Configurations;

public static class ConfigureServices
{
    public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder => builder
                    .WithOrigins("https://localhost:7028")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(MapperProfile));

        services.AddDbContext<CRM_KSKDbContext>(
            options => options
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            .UseCamelCaseNamingConvention());

        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAdminRepository, AdminRepositiry>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<ITrainerRepository, TrainerRepository>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
    }
}
