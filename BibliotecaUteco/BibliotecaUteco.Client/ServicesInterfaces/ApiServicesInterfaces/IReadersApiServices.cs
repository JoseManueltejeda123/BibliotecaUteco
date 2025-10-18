using BibliotecaUteco.Client.Requests.Readers.Actions;
using BibliotecaUteco.Client.Requests.Readers.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;

public interface IReadersApiServices
{
    Task<ApiResult<ReaderResponse>> CreateAsync(CreateReaderRequest request, CancellationToken cancellationToken = default);
    Task<ApiResult<List<ReaderResponse>>> GetByFilterAsync(GetReadersByFilterRequest request, CancellationToken cancellationToken = default);
}