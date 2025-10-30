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

    public async Task<ApiResponse<bool>> DeleteAsync(
        DeleteUserRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return await client.FetchDeleteAsync<bool>(
            UserEndpoint + $"/delete?{QueryStringBuilder.ToQueryString(request)}",
            cancellationToken
        );
    }

    public async Task<ApiResponse<UserResponse>> ChangeStateAsync(
        ChangeUserStateRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return await client.FetchPutAsync<UserResponse>(
            UserEndpoint + "/change-state",
            request,
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
        try
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(request.FullName), "fullName");
            form.Add(new StringContent(request.Password), "password");
            form.Add(new StringContent(request.Username), "userName");
            form.Add(new StringContent(request.SexId.ToString()), "sexId");

            form.Add(new StringContent(request.RoleId.ToString()), "roleId");
            form.Add(new StringContent(request.IdentityCardNumber), "identityCardNumber");

            if (request.ProfilePictureFile is not null)
            {
                var stream = request.ProfilePictureFile.OpenReadStream(
                    maxAllowedSize: FilesSettings.MaxFileSize
                );
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(
                    request.ProfilePictureFile.ContentType
                );
                form.Add(fileContent, "profilePictureFile", request.ProfilePictureFile.Name);
            }
            return await client.FetchPostAsync<UserResponse>(UserEndpoint, form, cancellationToken);
        }
        catch (Exception ex)
        {
            return new ApiResponse<UserResponse>()
            {
                IsSuccess = false,
                Data = null,
                Messages = new[] { ex.InnerException?.Message ?? ex.Message }.ToList(),
            };
        }
    }

    public async Task<ApiResponse<UserResponse>> UpdateAsync(
        UpdateUserRequest request,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(request.UserId.ToString()), "userId");
            form.Add(
                new StringContent(request.RemoveProfilePicture.ToString()),
                "removeProfilePicture"
            );
            if (!string.IsNullOrEmpty(request.CurrentPassword))
            {
                form.Add(new StringContent(request.CurrentPassword), "currentPassword");
            }

            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                form.Add(new StringContent(request.NewPassword), "newPassword");
            }

            form.Add(new StringContent(request.FullName), "fullName");
            form.Add(new StringContent(request.SexId.ToString()), "sexId");
            form.Add(new StringContent(request.Username), "userName");
            form.Add(new StringContent(request.IdentityCardNumber), "identityCardNumber");

            if (request.ProfilePictureFile is not null)
            {
                var stream = request.ProfilePictureFile.OpenReadStream(
                    maxAllowedSize: FilesSettings.MaxFileSize
                );
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(
                    request.ProfilePictureFile.ContentType
                );
                form.Add(fileContent, "profilePictureFile", request.ProfilePictureFile.Name);
            }
            return await client.FetchPutAsync<UserResponse>(UserEndpoint, form, cancellationToken);
        }
        catch (Exception ex)
        {
            return new ApiResponse<UserResponse>()
            {
                IsSuccess = false,
                Data = null,
                Messages = new[] { ex.InnerException?.Message ?? ex.Message }.ToList(),
            };
        }
    }
}
