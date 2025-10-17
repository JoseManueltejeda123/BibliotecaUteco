using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Readers.Actions;

public class CreateReaderRequest
{
    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El nombre completo no puede tener más de 50 caracteres.")]
    [MinLength(5, ErrorMessage = "El nombre completo debe tener al menos 5 caracteres.")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
    [MaxLength(10, ErrorMessage = "El número de teléfono debe tener exactamente 10 dígitos.")]
    [MinLength(10, ErrorMessage = "El número de teléfono debe tener exactamente 10 dígitos.")]
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [MaxLength(100, ErrorMessage = "La dirección no puede superar los 100 caracteres.")]
    [MinLength(10, ErrorMessage = "La dirección debe tener al menos 10 caracteres.")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "El número de cédula es obligatorio.")]
    [MaxLength(11, ErrorMessage = "El número de cédula debe tener exactamente 11 dígitos.")]
    [MinLength(11, ErrorMessage = "El número de cédula debe tener exactamente 11 dígitos.")]
    public string IdentityCardNumber { get; set; } = null!;

    [MaxLength(9, ErrorMessage = "La matrícula estudiantil no puede tener más de 9 caracteres.")]
    [MinLength(3, ErrorMessage = "La matrícula estudiantil debe tener al menos 3 caracteres.")]
    public string? StudentLicence { get; set; }

}