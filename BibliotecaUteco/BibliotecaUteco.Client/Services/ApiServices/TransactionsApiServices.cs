using System.Web;
using BibliotecaUteco.Client.Requests.Transactions.Actions;
using BibliotecaUteco.Client.Requests.Transactions.Queries;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;
using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Services.ApiServices;

public class TransactionsApiServices(BibliotecaHttpClient client) : ITransactionsApiServices
{
    private const string TransactionsEndpoint = "/transactions";

    public async Task<ApiResponse<List<TransactionResponse>>> GetByFilterAsync(
        GetTransactionsByFilterRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        if (!string.IsNullOrEmpty(request.UserName))
        {
            query["userName"] = request.UserName;
        }
        query["skip"] = request.Skip.ToString();
        query["take"] = request.Take.ToString();

        string queryString = query?.ToString() ?? "";

        return await client.FetchGetAsync<List<TransactionResponse>>(
            TransactionsEndpoint + $"/by-filter?{queryString}",
            cancellationToken
        );
    }

    public async Task<ApiResponse<CashBoxSummaryResponse>> GetSummaryAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await client.FetchGetAsync<CashBoxSummaryResponse>(
            TransactionsEndpoint + $"/summary",
            cancellationToken
        );
    }

    public async Task<ApiResponse<TransactionResponse>> RetireAsync(
        CreateTransactionRetirementRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return await client.FetchPostAsync<TransactionResponse>(
            TransactionsEndpoint + $"/retirement",
            request,
            cancellationToken
        );
    }
}
