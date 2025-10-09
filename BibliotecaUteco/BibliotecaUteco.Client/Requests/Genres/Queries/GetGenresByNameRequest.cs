using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Client.Requests.Genres.Queries
{
    public class GetGenresByNameRequest 
    {
        [MaxLength(25, ErrorMessage="El nombre del genero literario no debe de exceder los 25 caracteres")]
        public string? GenreName { get; set; }
        
    }
}