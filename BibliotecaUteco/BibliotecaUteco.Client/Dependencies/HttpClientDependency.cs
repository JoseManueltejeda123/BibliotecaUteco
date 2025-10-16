using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Dependencies;

public static class HttpClientDependency
{
   public static IServiceCollection AddHttpClientService(this IServiceCollection services, string uri)
   {
      services.AddHttpClient<BibliotecaHttpClient>((serviceProvider, client) =>
      {

         client.BaseAddress = new Uri(uri);
      });
      
      return services;
   }
}