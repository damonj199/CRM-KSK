using CRM_KSK.Blazor;
using CRM_KSK.Blazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });
builder.Services.AddScoped<ClientServiceBlazor>();
builder.Services.AddScoped<TrainerServiceBlazor>();
builder.Services.AddSingleton<ClientStateService>();
builder.Services.AddSingleton<TrainerStateService>();

await builder.Build().RunAsync();
