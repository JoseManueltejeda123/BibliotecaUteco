using BibliotecaUteco.Client.Services.ApiServices;
using BibliotecaUteco.Client.ServicesInterfaces;
using BibliotecaUteco.Client.ServicesInterfaces.ApiServices;
using BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;

namespace BibliotecaUteco.Client.Dependencies;

public static class ApiServicesDependencies
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersApiServices, UsersApiServices>();
        services.AddScoped<IAuthorsApiServices, AuthorsApiServices>();
        return services;
    }
}