using BibliotecaUteco.Client.Requests.Users.Actions;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;

public interface IUsersApiServices
{
    Task<ApiResult<JwtResponse>> LoginUserAsync(AuthenticateUserRequest request);
}