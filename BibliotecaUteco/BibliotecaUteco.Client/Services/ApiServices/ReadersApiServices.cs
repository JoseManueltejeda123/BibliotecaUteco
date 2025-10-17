using BibliotecaUteco.Client.Requests.Readers.Actions;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.Services.ApiServicesInterfaces;
using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Services.ApiServices;

public class ReadersApiServices(BibliotecaHttpClient client) : IReadersApiServices
{
    private const string AuthorsEndpoint = "/readers";

    public async Task<ApiResult<ReaderResponse>> CreateAsync(CreateReaderRequest request)
    {
        return await client.FetchPostAsync<ReaderResponse>(AuthorsEndpoint, request);
    }

}