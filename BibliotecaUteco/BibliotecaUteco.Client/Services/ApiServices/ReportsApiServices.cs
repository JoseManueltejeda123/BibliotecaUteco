using BibliotecaUteco.Client.Requests.Reports.Books;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;
using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Services.ApiServices;



public class ReportsApiServices(BibliotecaHttpClient client) : IReportsApiServices
{
    private const string ReportsEndpoint = "/reports";

    public async Task<ApiResponse<TopBooksResponse>> GetTopBooksByDateAsync(
        GetTopBooksByDateRequest request,
        CancellationToken cancellationToken = default
    )
    {
            
        return await client.FetchGetAsync<TopBooksResponse>(
            ReportsEndpoint + $"/top-books-by-date?{QueryStringBuilder.ToQueryString(request)}",
            cancellationToken
        );
    }
    
    public async Task<ApiResponse<LoansPerMonthResponse>> GetLoansPerMonthAsync(
        GetLoansPerMonthRequest request,
        CancellationToken cancellationToken = default
    )
    {
            
        return await client.FetchGetAsync<LoansPerMonthResponse>(
            ReportsEndpoint + $"/loans-per-month?{QueryStringBuilder.ToQueryString(request)}",
            cancellationToken
        );
    }
}