using BibliotecaUteco.Dependencies;

namespace BibliotecaUteco;

public static class DependencyInjection
{
    public static IServiceCollection AddServerServices(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        
        services.AddBibliotecaUtecoDbContextServices(builder.Configuration);
        services.AddJwtServices(builder.Configuration);
        services.AddEndpointDependencies();
        return services;
    }
}