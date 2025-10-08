using BibliotecaUteco.Client.Requests.Authors.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServices
{
    public interface IAuthorsApiServices
    {
        Task<ApiResult<List<AuthorResponse>>> GetByNameAsync(GetAuthorsByNameRequest request);
    }
}