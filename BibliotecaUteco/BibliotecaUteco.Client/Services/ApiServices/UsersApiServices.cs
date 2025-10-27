using System.Net.Http.Headers;
using System.Web;
using BibliotecaUteco.Client.Requests.Users.Actions;
using BibliotecaUteco.Client.Requests.Users.Queries;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;
using BibliotecaUteco.Client.Settings;
using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Services.ApiServices;

public class UsersApiServices(BibliotecaHttpClient client) : IUsersApiServices
{
    private const string UserEndpoint = "/users";

    public async Task<ApiResponse<bool>> ResetPasswordAsync(
        ResetPasswordRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return await client.FetchPutAsync<bool>(
            UserEndpoint + "/reset-password",
            request,
            cancellationToken
        );
    }

    public async Task<ApiResponse<JwtResponse>> LoginUserAsync(
        AuthenticateUserRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return await client.FetchPostAsync<JwtResponse>(
            UserEndpoint + "/authenticate",
            request,
            cancellationToken
        );
    }

    public async Task<ApiResponse<List<UserResponse>>> GetByFilterAsync(
        GetUsersByFilterRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["username"] = request.Username;
        string queryString = query?.ToString() ?? "";
        return await client.FetchGetAsync<List<UserResponse>>(
            UserEndpoint + $"/by-filter?{queryString}",
            cancellationToken
        );
    }

    public async Task<ApiResponse<UserResponse>> GetByIdAsync(
        GetUserByIdRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["userId"] = request.UserId.ToString();
        string queryString = query?.ToString() ?? "";
        return await client.FetchGetAsync<UserResponse>(
            UserEndpoint + $"/by-id?{queryString}",
            cancellationToken
        );
    }

    public async Task<ApiResponse<UserResponse>> CreateAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return await client.FetchPostAsync<UserResponse>(
            UserEndpoint,
            request.ToMultipartFormData(),
            cancellationToken
        );
    }

    public async Task<ApiResponse<UserResponse>> UpdateAsync(
        UpdateUserRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return await client.FetchPutAsync<UserResponse>(
            UserEndpoint,
            request.ToMultipartFormData(),
            cancellationToken
        );
    }
}
