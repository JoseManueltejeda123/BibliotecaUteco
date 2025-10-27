using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Transactions.Actions;

public class CreateTransactionRetirementRequest
{
    [Required(ErrorMessage = "El monto es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 0.")]
    public double Amount { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [MaxLength(30, ErrorMessage = "La contraseña no puede tener más de 30 caracteres.")]
    public string Password { get; set; } = "";
}
