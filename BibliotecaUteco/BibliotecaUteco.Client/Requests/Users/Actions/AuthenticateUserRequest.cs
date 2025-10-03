using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Users.Actions;

public class AuthenticateUserRequest
{
    [MaxLength(15), MinLength(5), Required]
    public string Username { get; set; } = "";

    [MaxLength(30), MinLength(8), Required]
    public string Password { get; set; } = "";
}