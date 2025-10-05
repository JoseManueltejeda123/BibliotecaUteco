using BibliotecaUteco.Client.Dependencies;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BibliotecaUteco.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddClientServices(this IServiceCollection services, WebAssemblyHostBuilder builder)
    {
        services.AddHttpClientService(builder.HostEnvironment.BaseAddress);
        services.AddApiServices();
        return services;

    }
}