using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Users.Queries;

public class GetUserByIdRequest
{
    [Required, Range(1, int.MaxValue)]
    public int UserId {get; set;}
}