using BibliotecaUteco.Client.Requests.Users.Actions;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;
using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Services.ApiServices;

public class UsersApiServices(BibliotecaHttpClient client) : IUsersApiServices
{
    private const string UserEndpoint = "/users";
    
    public async Task<ApiResult<JwtResponse>> LoginUserAsync(AuthenticateUserRequest request)
    {
        return await client.FetchPostAsync<JwtResponse>(UserEndpoint + "/authenticate", request);
        
        
    }
}