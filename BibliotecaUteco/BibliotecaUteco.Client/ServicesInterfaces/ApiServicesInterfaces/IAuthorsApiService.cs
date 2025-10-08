using BibliotecaUteco.Client.Requests.Authors.Actions;
using BibliotecaUteco.Client.Requests.Authors.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces
{
    public interface IAuthorsApiServices
    {
        Task<ApiResult<List<AuthorResponse>>> GetByNameAsync(GetAuthorsByNameRequest request);
        Task<ApiResult<AuthorResponse>> CreateAuthorAsync(CreateAuthorRequest request);
    }
}