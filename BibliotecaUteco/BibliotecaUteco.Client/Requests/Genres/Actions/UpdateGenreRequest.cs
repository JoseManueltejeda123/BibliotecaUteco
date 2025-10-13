using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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