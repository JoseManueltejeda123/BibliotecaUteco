using System.Net.Http.Headers;
using System.Net.Http.Json;
using BibliotecaUteco.Client.Identity.Provider;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.Services.ApiServices;
using BibliotecaUteco.Client.ServicesInterfaces;
using BibliotecaUteco.Client.Utilities;
using Refit;

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