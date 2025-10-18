using BibliotecaUteco.Client.Requests.Genres.Actions;
using BibliotecaUteco.Client.Requests.Genres.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces
{
    public interface IGenresApiSevices
    {
        Task<ApiResult<GenreResponse>> CreateAsync(CreateGenreRequest request, CancellationToken cancellationToken = default);
        Task<ApiResult<List<GenreResponse>>> GetByNameAsync(GetGenresByNameRequest request, CancellationToken cancellationToken = default);
        Task<ApiResult<GenreResponse>> UpdateAsync(UpdateGenreRequest request, CancellationToken cancellationToken = default);

        Task<ApiResult<bool>> DeleteAsync(DeleteGenreRequest request, CancellationToken cancellationToken = default);
    }
}