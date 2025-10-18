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


        public async Task<ApiResult<bool>> DeleteBookAsync(DeleteBookRequest request, CancellationToken cancellationToken = default)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["bookId"] = request.BookId.ToString();
            string queryString = query?.ToString() ?? "";
            return await client.FetchDeleteAsync<bool>(BooksEndpoint + $"/delete?{queryString}", cancellationToken);
        }
        public async Task<ApiResult<BookResponse>> CreateBookAsync(CreateBookRequest request, CancellationToken cancellationToken = default)
        {

            var form = new MultipartFormDataContent();
            form.Add(new StringContent(request.Name), "name");
            form.Add(new StringContent(request.Synopsis), "synopsis");
            form.Add(new StringContent(request.Stock.ToString()), "stock");
            foreach (var id in request.Genres.Select(g => g.Id))
            {
                form.Add(new StringContent(id.ToString()), "genreIds");
            }

            foreach (var id in request.Authors.Select(a => a.Id))
            {
                form.Add(new StringContent(id.ToString()), "authorIds");
            }

            if (request.CoverFile is not null)
            {
                var stream = request.CoverFile.OpenReadStream(maxAllowedSize: FilesSettings.MaxFileSize);
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(request.CoverFile.ContentType);
                form.Add(fileContent, "coverFile", request.CoverFile.Name);
            }

            return await client.FetchPostAsync<BookResponse>(BooksEndpoint, form, cancellationToken);


        }
        
         public async Task<ApiResult<BookResponse>> UpdateBookAsync(UpdateBookRequest request, CancellationToken cancellationToken = default)
                {
        
                    var form = new MultipartFormDataContent();
                    form.Add(new StringContent(request.BookId.ToString()), "bookId");
                    form.Add(new StringContent(request.BookName), "bookName");
                    form.Add(new StringContent(request.Synopsis), "Synopsis");
                    form.Add(new StringContent(request.RemoveCover.ToString()), "removeCover");
                    form.Add(new StringContent(request.Stock.ToString()), "stock");
                    foreach (var id in request.genreIds)
                    {
                        form.Add(new StringContent(id.ToString()), "genreIds");
                    }
        
                    foreach (var id in request.authorIds)
                    {
                        form.Add(new StringContent(id.ToString()), "authorIds");
                    }
        
                    if (request.CoverFile is not null)
                    {
                        var stream = request.CoverFile.OpenReadStream(maxAllowedSize: FilesSettings.MaxFileSize);
                        var fileContent = new StreamContent(stream);
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(request.CoverFile.ContentType);
                        form.Add(fileContent, "coverFile", request.CoverFile.Name);
                    }
        
                    return await client.FetchPutAsync<BookResponse>(BooksEndpoint, form, cancellationToken);
        
        
                }

        public async Task<ApiResult<List<BookResponse>>> GetByFilterAsync(GetBooksByFilterRequest request, CancellationToken cancellationToken = default)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["bookName"] = request.BookName;
            query["authorName"] = request.AuthorName;
            query["genreName"] = request.GenreName;
            query["take"] = request.Take.ToString();
            query["skip"] = request.Skip.ToString();


            string queryString = query?.ToString() ?? "";
            return await client.FetchGetAsync<List<BookResponse>>(BooksEndpoint + $"/by-filter?{queryString}", cancellationToken);
        }
    }

}
    
    
