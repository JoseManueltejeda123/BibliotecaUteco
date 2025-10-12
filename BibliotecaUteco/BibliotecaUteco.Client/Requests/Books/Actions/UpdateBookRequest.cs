using System.ComponentModel.DataAnnotations;
using BibliotecaUteco.Client.Responses;
using Microsoft.AspNetCore.Components.Forms;

namespace BibliotecaUteco.Client.Requests.Books.Actions;

public class UpdateBookRequest
{
    [Required(ErrorMessage = "El ID del libro es obligatorio.")]
    public int BookId { get; set; }

    [Required(ErrorMessage = "El nombre del libro es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El nombre del libro no puede superar los 50 caracteres.")]
    [MinLength(1, ErrorMessage = "El nombre del libro debe tener al menos 1 carácter.")]
    public string BookName { get; set; } = "";

    public bool RemoveCover { get; set; } = false;

    [Required(ErrorMessage = "La sinopsis es obligatoria.")]
    [MinLength(10, ErrorMessage = "La sinopsis debe tener al menos 10 caracteres.")]
    [MaxLength(500, ErrorMessage = "La sinopsis no puede superar los 500 caracteres.")]
    public string Synopsis { get; set; } = null!;

    [Required(ErrorMessage = "El stock es obligatorio.")]
    [Range(0, 10000, ErrorMessage = "El stock debe estar entre 0 y 10,000 unidades.")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "Debe seleccionar al menos un género.")]
    [MinLength(1, ErrorMessage = "Debe haber al menos un género asociado.")]
    [MaxLength(5, ErrorMessage = "No puede haber más de 5 géneros por libro.")]
    public List<GenreResponse> Genres { get; set; } = new();

    [Required(ErrorMessage = "Debe seleccionar al menos un autor.")]
    [MaxLength(10, ErrorMessage = "No puede haber más de 10 autores por libro.")]
    public List<AuthorResponse> Authors { get; set; } = new();

    public IBrowserFile? CoverFile { get; set; } = null;

    public List<int> authorIds => Authors.Select(a => a.Id).ToList();
    public List<int> genreIds => Genres.Select(a => a.Id).ToList();
}
