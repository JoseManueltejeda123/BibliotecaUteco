using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Authors.Actions;

public class CreateAuthorRequest
{
    [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
    [MinLength(1, ErrorMessage = "El nombre debe tener al menos 1 carácter.")]
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string FullName { get; set; } = null!;
}