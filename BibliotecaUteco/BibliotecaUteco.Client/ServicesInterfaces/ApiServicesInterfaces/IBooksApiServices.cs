using BibliotecaUteco.Client.Requests.Books.Actions;
using BibliotecaUteco.Client.Requests.Books.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.Services.ApiServices;

public interface IBooksApiServices
{
    Task<ApiResult<BookResponse>> CreateBookAsync(CreateBookRequest request, CancellationToken cancellationToken = default);
    Task<ApiResult<List<BookResponse>>> GetByFilterAsync(GetBooksByFilterRequest request, CancellationToken cancellationToken = default);

    Task<ApiResult<bool>> DeleteBookAsync(DeleteBookRequest request, CancellationToken cancellationToken = default);
    Task<ApiResult<BookResponse>> UpdateBookAsync(UpdateBookRequest request, CancellationToken cancellationToken = default);
}