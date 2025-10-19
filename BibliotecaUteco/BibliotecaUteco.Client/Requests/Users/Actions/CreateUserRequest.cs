using System.ComponentModel.DataAnnotations;
using BibliotecaUteco.Client.Utilities;
using Microsoft.AspNetCore.Components.Forms;

namespace BibliotecaUteco.Client.Requests.Users.Actions;

public class CreateUserRequest
{
    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El nombre completo no puede tener más de 50 caracteres.")]
    [MinLength(5, ErrorMessage = "El nombre completo debe tener al menos 5 caracteres.")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    [MaxLength(15, ErrorMessage = "El nombre de usuario no puede tener más de 15 caracteres.")]
    [MinLength(5, ErrorMessage = "El nombre de usuario debe tener al menos 5 caracteres.")]
    [RegularExpression(
        @"^[a-zA-Z0-9._]+$",
        ErrorMessage = "El nombre de usuario solo puede contener letras, números, puntos y guiones bajos."
    )]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [MaxLength(30, ErrorMessage = "La contraseña no puede tener más de 30 caracteres.")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "El número de cédula es obligatorio.")]
    [MaxLength(11, ErrorMessage = "El número de cédula debe tener exactamente 11 dígitos.")]
    [MinLength(11, ErrorMessage = "El número de cédula debe tener exactamente 11 dígitos.")]
    public string IdentityCardNumber { get; set; } = null!;

    [Required(ErrorMessage = "El rol es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un rol válido.")]
    public int RoleId => RoleParser.ParseRole(_roleName);
    
    
    

    [Range(1, 2)] 
    public int SexId => SexParser.ParseSex(_sexName);
    public string SexName => SexId == 1 ? "boy" : "girl";

    public string _roleName { get; set; } = "Bibliotecario";
    public string _sexName { get; set; } = "Masculino";

    public IBrowserFile? ProfilePictureFile { get; set; }
}
