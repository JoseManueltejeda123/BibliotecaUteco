using BibliotecaUteco.Client.Requests.Users.Actions;
using BibliotecaUteco.Client.Requests.Users.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;

public interface IUsersApiServices
{
    Task<ApiResponse<bool>> ResetPasswordAsync(
        ResetPasswordRequest request,
        CancellationToken cancellationToken = default
    );
    Task<ApiResponse<JwtResponse>> LoginUserAsync(
        AuthenticateUserRequest request,
        CancellationToken cancellationToken = default
    );
    Task<ApiResponse<UserResponse>> CreateAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ApiResponse<List<UserResponse>>> GetByFilterAsync(
        GetUsersByFilterRequest request,
        CancellationToken cancellationToken = default
    );
    Task<ApiResponse<UserResponse>> UpdateAsync(
        UpdateUserRequest request,
        CancellationToken cancellationToken = default
    );
}
