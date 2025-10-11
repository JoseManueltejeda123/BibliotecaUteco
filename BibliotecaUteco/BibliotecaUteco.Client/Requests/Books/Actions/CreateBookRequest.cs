using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace BibliotecaUteco.Client.Requests.Books.Actions
{
   public class CreateBookRequest
    {
        [Required(ErrorMessage = "El nombre del libro es obligatorio.")]
        [MinLength(1, ErrorMessage = "El nombre del libro debe tener al menos 1 carácter.")]
        [MaxLength(50, ErrorMessage = "El nombre del libro no puede tener más de 50 caracteres.")]
        public string Name { get; set; } = null!;

        public IBrowserFile? CoverFile { get; set; } = null;
        
        

        [Required(ErrorMessage = "La sinopsis del libro es obligatoria.")]
        [MinLength(10, ErrorMessage = "La sinopsis debe tener al menos 10 caracteres.")]
        [MaxLength(500, ErrorMessage = "La sinopsis no puede tener más de 500 caracteres.")]
        public string Sinopsis { get; set; } = null!;

        public List<int> AuthorIds { get; set; } = new();

       
        public List<int> GenreIds { get; set; } = new();

        [Required(ErrorMessage = "Debe especificar la cantidad en stock.")]
        [Range(1, int.MaxValue, ErrorMessage = "El stock debe ser mayor que cero.")]
        public int Stock { get; set; } = 1;
    }

}