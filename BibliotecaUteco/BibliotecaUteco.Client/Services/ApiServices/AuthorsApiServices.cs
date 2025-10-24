using System.Web;
using BibliotecaUteco.Client.Requests.Authors.Actions;
using BibliotecaUteco.Client.Requests.Authors.Queries;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;
using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Services.ApiServices
{
    public class AuthorsApiServices(BibliotecaHttpClient client) : IAuthorsApiServices
    {
        private const string AuthorsEndpoint = "/authors";

        public async Task<ApiResponse<List<AuthorResponse>>> GetByNameAsync(
            GetAuthorsByNameRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["authorsName"] = request.AuthorsName;
            string queryString = query?.ToString() ?? "";
            return await client.FetchGetAsync<List<AuthorResponse>>(
                AuthorsEndpoint + $"/get-by-name?{queryString}",
                cancellationToken
            );
        }

        public async Task<ApiResponse<AuthorResponse>> CreateAuthorAsync(
            CreateAuthorRequest request,
            CancellationToken cancellationToken = default
        )
        {
            return await client.FetchPostAsync<AuthorResponse>(
                AuthorsEndpoint,
                request,
                cancellationToken
            );
        }

        public async Task<ApiResponse<AuthorResponse>> UpdateAsync(
            UpdateAuthorRequest request,
            CancellationToken cancellationToken = default
        )
        {
            return await client.FetchPutAsync<AuthorResponse>(
                AuthorsEndpoint,
                request,
                cancellationToken
            );
        }

        public async Task<ApiResponse<bool>> DeleteAsync(
            UpdateAuthorRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["authorId"] = request.AuthorId.ToString();
            string queryString = query?.ToString() ?? "";
            return await client.FetchDeleteAsync<bool>(
                AuthorsEndpoint + $"/delete?{queryString}",
                cancellationToken
            );
        }
    }
}
