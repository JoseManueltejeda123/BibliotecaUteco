using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Users.Queries;

public class GetUsersByFilterRequest
{
    [MaxLength(15)]
    public string? Username { get; set; } 
}