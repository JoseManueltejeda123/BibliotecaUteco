using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Genres.Actions
{
    public class UpdateGenreRequest
    {
        [MaxLength(25)]
        [MinLength(1)]
        [Required]
     
        public string GenreName { get; set; } = null!;

        [Range(1,int.MaxValue)]
        [Required]      
        public int GenreId { get; set; } 
    }
}