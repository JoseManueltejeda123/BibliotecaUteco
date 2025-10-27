using System.Net.Http.Headers;
using System.Web;
using BibliotecaUteco.Client.Requests.Books.Actions;
using BibliotecaUteco.Client.Requests.Books.Queries;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.Settings;
using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Services.ApiServices
{
    public class BooksApiServices(BibliotecaHttpClient client) : IBooksApiServices
    {
        private const string BooksEndpoint = "/books";

        public async Task<ApiResponse<bool>> DeleteBookAsync(
            DeleteBookRequest request,
            CancellationToken cancellationToken = default
        ) => await client.FetchDeleteAsync<bool>(
                BooksEndpoint + $"/delete?{QueryStringBuilder.ToQueryString(request)}",
                cancellationToken);
        

        public async Task<ApiResponse<BookResponse>> CreateBookAsync(
            CreateBookRequest request,
            CancellationToken cancellationToken = default
        )=> await client.FetchPostAsync<BookResponse>(
                BooksEndpoint,
                request.ToMultipartFormData(),
                cancellationToken);
        

        public async Task<ApiResponse<BookResponse>> UpdateBookAsync(
            UpdateBookRequest request,
            CancellationToken cancellationToken = default
        )=>  await client.FetchPutAsync<BookResponse>(BooksEndpoint, request.ToMultipartFormData(), cancellationToken);
        

        public async Task<ApiResponse<List<BookResponse>>> GetByFilterAsync(
            GetBooksByFilterRequest request,
            CancellationToken cancellationToken = default
        )=>  await client.FetchGetAsync<List<BookResponse>>(
                BooksEndpoint + $"/by-filter?{QueryStringBuilder.ToQueryString(request)}",
                cancellationToken);
        
    }
}
