using BibliotecaUteco.Client.Requests.Transactions.Actions;
using BibliotecaUteco.Client.Requests.Transactions.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;

public interface ITransactionsApiServices
{
    Task<ApiResponse<List<TransactionResponse>>> GetByFilterAsync(
        GetTransactionsByFilterRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ApiResponse<TransactionResponse>> RetireAsync(
        CreateTransactionRetirementRequest request,
        CancellationToken cancellationToken = default
    );
}
