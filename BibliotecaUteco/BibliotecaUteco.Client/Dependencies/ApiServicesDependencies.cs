using BibliotecaUteco.Client.Services.ApiServices;
using BibliotecaUteco.Client.Services.ApiServicesInterfaces;
using BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;

namespace BibliotecaUteco.Client.Dependencies;

public static class ApiServicesDependencies
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersApiServices, UsersApiServices>();
        services.AddScoped<IAuthorsApiServices, AuthorsApiServices>();
        services.AddScoped<IGenresApiSevices, GenresApiSevices>();
        services.AddScoped<IBooksApiServices, BooksApiServices>();
        services.AddScoped<IReadersApiServices, ReadersApiServices>();


        return services;
    }
}