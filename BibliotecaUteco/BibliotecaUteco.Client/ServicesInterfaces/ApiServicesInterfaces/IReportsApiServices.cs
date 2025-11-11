using BibliotecaUteco.Client.Requests.Reports.Books;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;
public interface IReportsApiServices
{
    Task<ApiResponse<TopBooksResponse>> GetTopBooksByDateAsync(
        GetTopBooksByDateRequest request,
        CancellationToken cancellationToken = default
    );
}