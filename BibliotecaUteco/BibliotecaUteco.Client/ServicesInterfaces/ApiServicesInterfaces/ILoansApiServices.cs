using BibliotecaUteco.Client.Requests.Loans.Actions;
using BibliotecaUteco.Client.Requests.Loans.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;

public interface ILoansApiServices
{
    Task<ApiResult<LoanResponse>> CreateAsync(
        CreateLoanRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ApiResult<List<LoanResponse>>> GetByFilterAsync(
        GetLoansByFilterRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ApiResult<LoanResponse>> MarkAsReturnedAsync(
        MarkLoanAsReturnedRequest request,
        CancellationToken cancellationToken = default
    );
}