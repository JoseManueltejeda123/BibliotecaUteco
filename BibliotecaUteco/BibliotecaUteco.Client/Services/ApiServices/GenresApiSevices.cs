using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BibliotecaUteco.Client.Requests.Genres.Actions;
using BibliotecaUteco.Client.Requests.Genres.Queries;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.Services.ApiServicesInterfaces;
using BibliotecaUteco.Client.Utilities;
using Refit;

namespace BibliotecaUteco.Client.Services.ApiServices
{

    public class GenresApiSevices(BibliotecaHttpClient client) : IGenresApiSevices
    {
        private const string GenresEndpoint = "/genres";


        public async Task<ApiResult<List<GenreResponse>>> GetByNameAsync(GetGenresByNameRequest request)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["genreName"] = request.GenreName;
            string queryString = query.ToString() ?? "";
            return await client.FetchGetAsync<List<GenreResponse>>(GenresEndpoint + $"/by-name?{queryString}");
        }

        public async Task<ApiResult<GenreResponse>> CreateAsync(CreateGenreRequest request)
        {

            return await client.FetchPostAsync<GenreResponse>(GenresEndpoint, request);
        }

    }
}