using BibliotecaUteco.Client.Requests.Books.Actions;
using BibliotecaUteco.Client.Requests.Books.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.Services.ApiServices;

public interface IBooksApiServices
{
    Task<ApiResult<BookResponse>> CreateBookAsync(CreateBookRequest request);
    Task<ApiResult<List<BookResponse>>> GetByFilterAsync(GetBooksByFilterRequest request);

    Task<ApiResult<bool>> DeleteBookAsync(DeleteBookRequest request);
    Task<ApiResult<BookResponse>> UpdateBookAsync(UpdateBookRequest request);
}