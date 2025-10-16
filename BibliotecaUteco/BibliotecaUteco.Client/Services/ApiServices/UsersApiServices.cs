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
    
    public async Task<ApiResult<JwtResponse>> LoginUserAsync(AuthenticateUserRequest request)
    {
        return await client.FetchPostAsync<JwtResponse>(UserEndpoint + "/authenticate", request);
        
        
    }
    
    public async Task<ApiResult<List<UserResponse>>> GetByFilterAsync(GetUsersByFilterRequest request)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["username"] = request.Username;
        string queryString = query?.ToString() ?? "";
        return await client.FetchGetAsync<List<UserResponse>>(UserEndpoint + $"/by-filter?{queryString}");
        
        
    }
    
    public async Task<ApiResult<UserResponse>> CreateAsync(CreateUserRequest request)
    {
        try
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(request.FullName), "fullName");
            form.Add(new StringContent(request.Password), "password");
            form.Add(new StringContent(request.Username), "userName");
            form.Add(new StringContent(request.RoleId.ToString()), "roleId");
            form.Add(new StringContent(request.IdentityCardNumber), "identityCardNumber");


            if (request.ProfilePictureFile is not null)
            {
                var stream = request.ProfilePictureFile.OpenReadStream(maxAllowedSize: FilesSettings.MaxFileSize);
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(request.ProfilePictureFile.ContentType);
                form.Add(fileContent, "profilePictureFile", request.ProfilePictureFile.Name);
            }
            return await client.FetchPostAsync<UserResponse>(UserEndpoint, form);


        }
        catch (Exception ex)
        {
            return ApiResult<UserResponse>.BuildFailure(HttpStatus.BadRequest, $"Ha ocurrido un error: {ex.Message}");
        }
       
    }
    public async Task<ApiResult<UserResponse>> UpdateAsync(UpdateUserRequest request)
    {
        try
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(request.UserId.ToString()), "userId");
            form.Add(new StringContent(request.RemoveProfilePicture.ToString()), "removeProfilePicture");
            form.Add(new StringContent(request.FullName), "fullName");
            form.Add(new StringContent(request.Username), "userName");
            form.Add(new StringContent(request.IdentityCardNumber), "identityCardNumber");


            if (request.ProfilePictureFile is not null)
            {
                var stream = request.ProfilePictureFile.OpenReadStream(maxAllowedSize: FilesSettings.MaxFileSize);
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(request.ProfilePictureFile.ContentType);
                form.Add(fileContent, "profilePictureFile", request.ProfilePictureFile.Name);
            }
            return await client.FetchPutAsync<UserResponse>(UserEndpoint, form);


        }
        catch (Exception ex)
        {
            return ApiResult<UserResponse>.BuildFailure(HttpStatus.BadRequest, $"Ha ocurrido un error: {ex.Message}");
        }
       
    }
}