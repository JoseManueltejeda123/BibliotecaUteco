using System.Web;
using BibliotecaUteco.Client.Requests.Transactions.Queries;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;
using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Services.ApiServices;

public class TransactionsApiServices(BibliotecaHttpClient client) : ITransactionsApiServices
{
    private const string TransactionsEndpoint = "/transactions";
    
    public async Task<ApiResult<List<TransactionResponse>>> GetByFilterAsync(
        GetTransactionsByFilterRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var query = HttpUtility.ParseQueryString(string.Empty);


        if (request.UserId.HasValue)
        {
            query["userId"] = request.UserId.Value.ToString();

        }
        query["skip"] = request.Skip.ToString();
        query["take"] = request.Take.ToString();

        string queryString = query?.ToString() ?? "";

        return await client.FetchGetAsync<List<TransactionResponse>>(
            TransactionsEndpoint + $"/by-filter?{queryString}",
            cancellationToken
          
        );
    }

}