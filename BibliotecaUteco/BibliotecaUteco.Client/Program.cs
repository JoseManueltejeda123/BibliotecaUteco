using BibliotecaUteco.Client;
using BibliotecaUteco.Client.Dependencies;
using BibliotecaUteco.Client.Identity.Provider;
using BibliotecaUteco.Client.Services;
using BibliotecaUteco.Client.ServicesInterfaces;
using Blazor.Sonner.Extensions;
using LumexUI.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSonner();
builder.Services.AddLumexServices();
builder.Services.AddClientServices(builder);
await builder.Build().RunAsync();
