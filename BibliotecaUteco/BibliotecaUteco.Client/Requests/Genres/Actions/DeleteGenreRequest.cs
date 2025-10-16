using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Genres.Actions
{
    public class DeleteGenreRequest
    {
        [Range(1,int.MaxValue)]
        [Required]      
        public int GenreId { get; set; } 
    }
}