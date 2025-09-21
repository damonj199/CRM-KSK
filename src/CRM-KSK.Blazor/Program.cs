using CRM_KSK.Blazor;
using CRM_KSK.Blazor.Auth;
using CRM_KSK.Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.HostEnvironment.Environment}.json", optional: true, reloadOnChange: true);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiUrl"]) })
    ;
builder.Services.AddScoped<ClientServiceBlazor>();
builder.Services.AddScoped<TrainerServiceBlazor>();
builder.Services.AddScoped<ScheduleServiceBlazor>();
builder.Services.AddScoped<TrainingServiceBlazor>();
builder.Services.AddScoped<MembershipServiceBlazor>();
builder.Services.AddScoped<HorsesServiceBlazor>();
builder.Services.AddScoped<MembershipDeductionLogServiceBlazor>();

builder.Services.AddTransient<JwtAuthorizationMessageHandler>();
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]);
})
.AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

builder.Services.AddSingleton<ClientStateService>();
builder.Services.AddSingleton<TrainerStateService>();

// Radzen services
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

// Настройка JSON опций для правильной десериализации camelCase
builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.PropertyNameCaseInsensitive = true;
});


await builder.Build().RunAsync();