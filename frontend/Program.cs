global using wet_ui.Services;
global using wet_ui.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using wet_ui;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
string apiBaseUrl = "https://localhost:7117";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new DeviceService(new HttpClient { BaseAddress = new Uri(apiBaseUrl) }));
builder.Logging.SetMinimumLevel(LogLevel.Debug);
builder.Services.AddLogging();

await builder.Build().RunAsync();
