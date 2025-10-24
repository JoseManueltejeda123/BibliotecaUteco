using BibliotecaUteco.Client.Requests.Genres.Actions;
using BibliotecaUteco.Client.Requests.Genres.Queries;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.ServicesInterfaces.ApiServicesInterfaces
{
    public interface IGenresApiSevices
    {
        Task<ApiResponse<GenreResponse>> CreateAsync(
            CreateGenreRequest request,
            CancellationToken cancellationToken = default
        );
        Task<ApiResponse<List<GenreResponse>>> GetByNameAsync(
            GetGenresByNameRequest request,
            CancellationToken cancellationToken = default
        );
        Task<ApiResponse<GenreResponse>> UpdateAsync(
            UpdateGenreRequest request,
            CancellationToken cancellationToken = default
        );

        Task<ApiResponse<bool>> DeleteAsync(
            DeleteGenreRequest request,
            CancellationToken cancellationToken = default
        );
    }
}
