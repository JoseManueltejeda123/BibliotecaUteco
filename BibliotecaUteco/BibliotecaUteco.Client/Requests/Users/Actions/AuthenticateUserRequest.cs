using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Users.Actions;

public class AuthenticateUserRequest
{
    [MaxLength(15, ErrorMessage = "El nombre de usuario no puede superar los 15 caracteres"), MinLength(5, ErrorMessage = "El nombre de usuario debe de ser mayor a 5 caracteres"), Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    public string Username { get; set; } = "";
    
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [MaxLength(30, ErrorMessage = "La contraseña no puede tener más de 30 caracteres.")]
    public string Password { get; set; } = "";

}