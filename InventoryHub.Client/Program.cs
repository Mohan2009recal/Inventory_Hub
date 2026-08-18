using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using InventoryHub.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<InventoryHub.Client.Pages.Index>("#app");

// Base URL points to running Web API instance
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000/") });

await builder.Build().RunAsync();