namespace CRM_KSK.Api;

public static class ConfigureServices
{
    public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                builder => builder
                    .WithOrigins("http://localhost:5000")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
