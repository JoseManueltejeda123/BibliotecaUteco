using BibliotecaUteco.Client.Requests.Readers.Actions;
using BibliotecaUteco.Client.Requests.Readers.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;

public interface IReadersApiServices
{
    Task<ApiResponse<ReaderResponse>> CreateAsync(
        CreateReaderRequest request,
        CancellationToken cancellationToken = default
    );
    Task<ApiResponse<List<ReaderResponse>>> GetByFilterAsync(
        GetReadersByFilterRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ApiResponse<ReaderResponse>> UpdateAsync(
        UpdateReaderRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ApiResponse<bool>> DeleteAsync(
        DeleteReaderRequest request,
        CancellationToken cancellationToken = default
    );
}
