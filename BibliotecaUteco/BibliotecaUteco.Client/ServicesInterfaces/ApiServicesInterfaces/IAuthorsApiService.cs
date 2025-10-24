using BibliotecaUteco.Client.Requests.Authors.Actions;
using BibliotecaUteco.Client.Requests.Authors.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces
{
    public interface IAuthorsApiServices
    {
        Task<ApiResponse<List<AuthorResponse>>> GetByNameAsync(
            GetAuthorsByNameRequest request,
            CancellationToken cancellationToken = default
        );
        Task<ApiResponse<AuthorResponse>> CreateAuthorAsync(
            CreateAuthorRequest request,
            CancellationToken cancellationToken = default
        );

        Task<ApiResponse<AuthorResponse>> UpdateAsync(
            UpdateAuthorRequest request,
            CancellationToken cancellationToken = default
        );
        Task<ApiResponse<bool>> DeleteAsync(
            UpdateAuthorRequest request,
            CancellationToken cancellationToken = default
        );
    }
}
