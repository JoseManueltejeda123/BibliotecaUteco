using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Client.Requests.Genres.Actions
{
    public class CreateGenreRequest
    {
        [MaxLength(25, ErrorMessage = "El nombre del genero literario no puede tener más de 25 caracteres.")]
        [MinLength(1, ErrorMessage = "El nombre del genero literario debe tener al menos 1 carácter.")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Name { get; set; } = null!;
    }
}