using System.Reflection;
using BibliotecaUteco.Features;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BibliotecaUteco.Dependencies;

public static class EndpointDependecies
{
    public static IServiceCollection AddEndpointDependencies(this IServiceCollection services)
    {
        services.AddTransient(typeof(IEndpointWrapper<>), typeof(EndpointWrapper<>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        //registramos los handers de los comandos
        var handlerInterfaceType = typeof(ICommandHandler<,>);
        var handlerType = Assembly
            .GetCallingAssembly()
            .GetTypes()
            .Where(type => !type.IsAbstract && !type.IsInterface)
            .SelectMany(type =>
                type.GetInterfaces()
                    .Where(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType
                    )
                    .Select(i => new { Interface = i, Implementation = type })
            );
        
        foreach (var handler in handlerType)
        {
            services.AddScoped(handler.Interface, handler.Implementation);
        }
        
        services.AddTransient<ISender, Sender>();
        
        ServiceDescriptor[] endpointServiceDescriptors = Assembly
            .GetExecutingAssembly()
            .DefinedTypes.Where(type =>
                type is { IsAbstract: false, IsGenericType: false, IsGenericTypeDefinition: false }
                && typeof(IEndpoint).IsAssignableFrom(type)
            )
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(endpointServiceDescriptors);
        
        return services;


    }
    
    public static IApplicationBuilder MapEndpoints(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            foreach (var endpoint in app.ApplicationServices.GetServices<IEndpoint>())
            {
                endpoint.MapEndpoint(endpoints);
            }
        });

        return app;
    }
}