using BibliotecaUteco.Client.Requests.Authors.Actions;
using BibliotecaUteco.Client.Requests.Authors.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces
{
    public interface IAuthorsApiServices
    {
        Task<ApiResult<List<AuthorResponse>>> GetByNameAsync(GetAuthorsByNameRequest request, CancellationToken cancellationToken = default);
        Task<ApiResult<AuthorResponse>> CreateAuthorAsync(CreateAuthorRequest request, CancellationToken cancellationToken = default);

        Task<ApiResult<AuthorResponse>> UpdateAsync(UpdateAuthorRequest request, CancellationToken cancellationToken = default);
        Task<ApiResult<bool>> DeleteAsync(UpdateAuthorRequest request, CancellationToken cancellationToken = default);
    }
}