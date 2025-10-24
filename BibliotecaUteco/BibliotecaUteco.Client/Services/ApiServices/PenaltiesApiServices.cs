using System.Web;
using BibliotecaUteco.Client.Requests.Penalties.Actions;
using BibliotecaUteco.Client.Requests.Penalties.Queries;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;
using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Services.ApiServices;

public class PenaltiesApiServices(BibliotecaHttpClient client) : IPenaltiesApiServices
{
    private const string PenaltiesEndpoint = "/penalties";

    public async Task<ApiResponse<List<PenaltyResponse>>> GetByFilterAsync(
        GetPenaltiesByFilterRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        
        query["take"] = request.Take.ToString();
        query["skip"] = request.Skip.ToString();
        query["loanId"] = request.LoanId.ToString();
        
        if(request.IsDue.HasValue)
        {
            query["isDue"] =  request.IsDue.Value.ToString();

        }
        
        string queryString = query.ToString() ?? "";
        return await client.FetchGetAsync<List<PenaltyResponse>>(
            PenaltiesEndpoint + $"/by-filter?{queryString}",
            cancellationToken
        );
    }
    public async Task<ApiResponse<PenaltyResponse>> PayAsync(
        PayPenaltyRequest request,
        CancellationToken cancellationToken = default
    )
    {
       
        return await client.FetchPutAsync<PenaltyResponse>(
            PenaltiesEndpoint + $"/pay",
            request,
            cancellationToken
        );
    }
}