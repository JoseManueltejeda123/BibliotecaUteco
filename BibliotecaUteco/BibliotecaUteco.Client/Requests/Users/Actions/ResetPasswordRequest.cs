using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Users.Actions;

public class ResetPasswordRequest
{
    public int UserId { get; set; }
}
