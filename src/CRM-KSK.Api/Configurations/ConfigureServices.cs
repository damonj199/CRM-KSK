using CRM_KSK.Api.Extensions;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Application.Services;
using CRM_KSK.Dal.PostgreSQL;
using CRM_KSK.Dal.PostgreSQL.Repositories;
using CRM_KSK.Infrastructure;
using CRM_KSK.Infrastructure.BackgroundServices;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace CRM_KSK.Api.Configurations;

public static class ConfigureServices
{
    public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.AddApiAuthentication(configuration);

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.WithOrigins("http://localhost:8080")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(MapperProfile));

        services.AddDbContext<CRM_KSKDbContext>(
            options => options
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            .UseCamelCaseNamingConvention());

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthRepository, AuthRepositiry>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<ITrainerRepository, TrainerRepository>();
        services.AddScoped<ITrainingService, TrainingService>();
        services.AddScoped<ITrainingRepository, TrainingRepository>();
        services.AddScoped<IMembershipService, MembershipService>();
        services.AddScoped<IMembershipRepository, MembershipRepository>();
        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IProcessBirthdays, ProcessBirthdays>();
        services.AddSingleton<IWorkWithMembership, WorkWithMembership>();
        services.AddHostedService<BirthdayNotificationBackgroundService>();
    }
}
