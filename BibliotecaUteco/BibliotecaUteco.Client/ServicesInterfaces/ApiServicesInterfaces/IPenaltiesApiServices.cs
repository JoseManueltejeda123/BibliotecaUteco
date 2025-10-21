using BibliotecaUteco.Client.Requests.Penalties.Actions;
using BibliotecaUteco.Client.Requests.Penalties.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;

public interface IPenaltiesApiServices
{
    Task<ApiResult<List<PenaltyResponse>>> GetByFilterAsync(
        GetPenaltiesByFilterRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ApiResult<PenaltyResponse>> PayAsync(
        PayPenaltyRequest request,
        CancellationToken cancellationToken = default
    );
}