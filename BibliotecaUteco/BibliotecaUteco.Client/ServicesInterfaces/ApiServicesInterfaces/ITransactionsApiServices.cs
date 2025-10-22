using BibliotecaUteco.Client.Requests.Transactions.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;

public interface ITransactionsApiServices
{
    Task<ApiResult<List<TransactionResponse>>> GetByFilterAsync(
        GetTransactionsByFilterRequest request,
        CancellationToken cancellationToken = default
    );
}