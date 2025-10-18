using BibliotecaUteco.Client.Requests.Users.Actions;
using BibliotecaUteco.Client.Requests.Users.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;

public interface IUsersApiServices
{
    Task<ApiResult<JwtResponse>> LoginUserAsync(AuthenticateUserRequest request, CancellationToken cancellationToken = default);
    Task<ApiResult<UserResponse>> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default);

    Task<ApiResult<List<UserResponse>>> GetByFilterAsync(GetUsersByFilterRequest request, CancellationToken cancellationToken = default);
   Task<ApiResult<UserResponse>> UpdateAsync(UpdateUserRequest request, CancellationToken cancellationToken = default);

}