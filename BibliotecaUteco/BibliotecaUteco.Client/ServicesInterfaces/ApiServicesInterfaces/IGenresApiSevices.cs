using BibliotecaUteco.Client.Requests.Genres.Actions;
using BibliotecaUteco.Client.Requests.Genres.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.Services.ApiServicesInterfaces
{
    public interface IGenresApiSevices
    {
        Task<ApiResult<GenreResponse>> CreateAsync(CreateGenreRequest request);
        Task<ApiResult<List<GenreResponse>>> GetByNameAsync(GetGenresByNameRequest request);
        Task<ApiResult<GenreResponse>> UpdateAsync(UpdateGenreRequest request);

        Task<ApiResult<bool>> DeleteAsync(DeleteGenreRequest request);
    }
}