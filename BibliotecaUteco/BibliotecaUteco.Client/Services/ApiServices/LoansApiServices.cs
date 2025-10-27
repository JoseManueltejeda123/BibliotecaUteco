using System.Web;
using BibliotecaUteco.Client.Requests.Loans.Actions;
using BibliotecaUteco.Client.Requests.Loans.Queries;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;
using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Services.ApiServices;

public class LoansApiServices(BibliotecaHttpClient client) : ILoansApiServices
{
    private const string LoansEndpoint = "/loans";

    public async Task<ApiResponse<LoanResponse>> CreateAsync(
        CreateLoanRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return await client.FetchPostAsync<LoanResponse>(LoansEndpoint, request, cancellationToken);
    }

    public async Task<ApiResponse<LoanResponse>> MarkAsReturnedAsync(
        MarkLoanAsReturnedRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return await client.FetchPutAsync<LoanResponse>(
            LoansEndpoint + "/mark-as-returned",
            request,
            cancellationToken
        );
    }

    public async Task<ApiResponse<List<LoanResponse>>> GetByFilterAsync(
        GetLoansByFilterRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["justExceededOnes"] = request.JustExceededOnes.ToString();
        query["justReturnedOnes"] = request.JustReturnedOnes.ToString();
        query["justPendingOnes"] = request.JustPendingOnes.ToString();
        query["take"] = request.Take.ToString();
        query["skip"] = request.Skip.ToString();
        query["IdentityCardNumber"] = request.IdentityCardNumber;
        query["studentLicence"] = request.StudentLicence;

        string queryString = query.ToString() ?? "";
        return await client.FetchGetAsync<List<LoanResponse>>(
            LoansEndpoint + $"/by-filter?{queryString}",
            cancellationToken
        );
    }
}
