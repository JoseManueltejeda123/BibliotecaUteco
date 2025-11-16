using BibliotecaUteco.Client.Requests.Summaries.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.Services.ApiServicesInterfaces;

public interface IApplicationSummariesApiServices
{
    Task<ApiResponse<ApplicationSummaryResponse>> GetByDateAsync(
        GetApplicationSummaryRequest request,
        CancellationToken cancellationToken = default
    );
}