using BibliotecaUteco.Client.Requests.Reports;
using BibliotecaUteco.Client.Requests.Reports.Books;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;
public interface IReportsApiServices
{
    Task<ApiResponse<TopBooksResponse>> GetTopBooksByDateAsync(
        GetTopBooksByDateRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ApiResponse<LoansPerMonthResponse>> GetLoansPerMonthAsync(
        GetLoansPerMonthRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ApiResponse<GeneralReport>> GetGeneralAsync(
        GetGeneralReportRequest request,
        CancellationToken cancellationToken = default
    );
}