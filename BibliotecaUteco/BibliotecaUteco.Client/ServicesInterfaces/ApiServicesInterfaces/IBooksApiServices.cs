using BibliotecaUteco.Client.Requests.Books.Actions;
using BibliotecaUteco.Client.Requests.Books.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.Services.ApiServices;

public interface IBooksApiServices
{
    Task<ApiResponse<BookResponse>> CreateBookAsync(
        CreateBookRequest request,
        CancellationToken cancellationToken = default
    );
    Task<ApiResponse<List<BookResponse>>> GetByFilterAsync(
        GetBooksByFilterRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ApiResponse<bool>> DeleteBookAsync(
        DeleteBookRequest request,
        CancellationToken cancellationToken = default
    );
    Task<ApiResponse<BookResponse>> UpdateBookAsync(
        UpdateBookRequest request,
        CancellationToken cancellationToken = default
    );
}
