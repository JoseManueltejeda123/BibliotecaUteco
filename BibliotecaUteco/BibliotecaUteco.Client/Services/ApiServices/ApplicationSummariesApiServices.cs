using BibliotecaUteco.Client.Requests.Summaries.Queries;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.Services.ApiServicesInterfaces;
using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Services.ApiServices
{
    public class ApplicationSummariesApiServices(BibliotecaHttpClient client) : IApplicationSummariesApiServices
    {
        private const string SummaryEndpoint = "/summary";

        public async Task<ApiResponse<ApplicationSummaryResponse>> GetByDateAsync(
            GetApplicationSummaryRequest request,
            CancellationToken cancellationToken = default
        )
        {
            
            return await client.FetchGetAsync<ApplicationSummaryResponse>(
                SummaryEndpoint + $"/by-date?{QueryStringBuilder.ToQueryString(request)}",
                cancellationToken
            );
        }
    }
}