using CRM_KSK.Blazor;
using CRM_KSK.Blazor.Auth;
using CRM_KSK.Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
{
    var client = new HttpClient { BaseAddress = new Uri("https://localhost:7268") };
    client.DefaultRequestHeaders.Add("Accept", "application/json");

    return client;
});
builder.Services.AddScoped<ClientServiceBlazor>();
builder.Services.AddScoped<TrainerServiceBlazor>();
builder.Services.AddScoped<ScheduleServiceBlazor>();
builder.Services.AddScoped<TrainingServiceBlazor>();
builder.Services.AddScoped<MembershipServiceBlazor>();
builder.Services.AddScoped<JwtAuthorizationMessageHandler>();
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7268");
})
.AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

builder.Services.AddSingleton<ClientStateService>();
builder.Services.AddSingleton<TrainerStateService>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
