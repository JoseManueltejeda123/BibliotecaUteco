using BibliotecaUteco.Dependencies;
using BibliotecaUteco.Services;

namespace BibliotecaUteco;

public static class DependencyInjection
{
    public static IServiceCollection AddServerServices(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        services.AddScoped<IFileUploadService, FileUploadService>();
        services.AddBibliotecaUtecoDbContextServices(builder.Configuration);
        services.AddJwtServices(builder.Configuration);
        services.AddEndpointDependencies();
        return services;
    }
}