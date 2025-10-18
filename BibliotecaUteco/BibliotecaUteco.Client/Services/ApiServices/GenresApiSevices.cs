using System.Web;
using BibliotecaUteco.Client.Requests.Genres.Actions;
using BibliotecaUteco.Client.Requests.Genres.Queries;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.Services.ApiServicesInterfaces;
using BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;
using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Services.ApiServices
{

    public class GenresApiSevices(BibliotecaHttpClient client) : IGenresApiSevices
    {
        private const string GenresEndpoint = "/genres";


        public async Task<ApiResult<List<GenreResponse>>> GetByNameAsync(GetGenresByNameRequest request, CancellationToken cancellationToken = default)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["genreName"] = request.GenreName;
            string queryString = query.ToString() ?? "";
            return await client.FetchGetAsync<List<GenreResponse>>(GenresEndpoint + $"/by-name?{queryString}", cancellationToken);
        }

        public async Task<ApiResult<GenreResponse>> CreateAsync(CreateGenreRequest request, CancellationToken cancellationToken = default)
        {

            return await client.FetchPostAsync<GenreResponse>(GenresEndpoint, request, cancellationToken);
        }

        public async Task<ApiResult<GenreResponse>> UpdateAsync(UpdateGenreRequest request, CancellationToken cancellationToken = default)
        {

            return await client.FetchPutAsync<GenreResponse>(GenresEndpoint, request, cancellationToken);
        }

         public async Task<ApiResult<bool>> DeleteAsync(DeleteGenreRequest request, CancellationToken cancellationToken = default)
        {

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["genreId"] = request.GenreId.ToString();
            string queryString = query.ToString() ?? "";
            return await client.FetchDeleteAsync<bool>(GenresEndpoint + $"/delete?{queryString}", cancellationToken);
        }

    }
}