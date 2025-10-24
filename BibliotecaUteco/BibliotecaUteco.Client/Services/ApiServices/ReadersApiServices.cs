using System.Web;
using BibliotecaUteco.Client.Requests.Readers.Actions;
using BibliotecaUteco.Client.Requests.Readers.Queries;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.Services.ApiServicesInterfaces;
using BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces;
using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Services.ApiServices;

public class ReadersApiServices(BibliotecaHttpClient client) : IReadersApiServices
{
    private const string ReadersEndpoint = "/readers";

    public async Task<ApiResponse<ReaderResponse>> CreateAsync(
        CreateReaderRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return await client.FetchPostAsync<ReaderResponse>(
            ReadersEndpoint,
            request,
            cancellationToken
        );
    }

    public async Task<ApiResponse<bool>> DeleteAsync(
        DeleteReaderRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

    
        query["readerId"] = request.ReaderId.ToString();
        string queryString = query?.ToString() ?? "";

        return await client.FetchDeleteAsync<bool>(
            ReadersEndpoint + $"/delete?{queryString}",
            cancellationToken
          
        );
    }

    public async Task<ApiResponse<ReaderResponse>> UpdateAsync(
        UpdateReaderRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return await client.FetchPutAsync<ReaderResponse>(
            ReadersEndpoint,
            request,
            cancellationToken
        );
    }

    public async Task<ApiResponse<List<ReaderResponse>>> GetByFilterAsync(
        GetReadersByFilterRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["studentLicence"] = request.StudentLicence;
        query["identityCardNumber"] = request.IdentityCardNumber;
        query["skip"] = request.Skip.ToString();
        query["take"] = request.Take.ToString();

        string queryString = query?.ToString() ?? "";
        return await client.FetchGetAsync<List<ReaderResponse>>(
            ReadersEndpoint + $"/by-filter?{queryString}",
            cancellationToken
        );
    }
}
