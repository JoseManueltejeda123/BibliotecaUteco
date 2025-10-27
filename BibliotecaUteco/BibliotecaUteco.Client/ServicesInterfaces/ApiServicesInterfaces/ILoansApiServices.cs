using BibliotecaUteco.Client.Requests.Loans.Actions;
using BibliotecaUteco.Client.Requests.Loans.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;

public interface ILoansApiServices
{
    Task<ApiResponse<LoanResponse>> CreateAsync(
        CreateLoanRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ApiResponse<List<LoanResponse>>> GetByFilterAsync(
        GetLoansByFilterRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ApiResponse<LoanResponse>> MarkAsReturnedAsync(
        MarkLoanAsReturnedRequest request,
        CancellationToken cancellationToken = default
    );
}
