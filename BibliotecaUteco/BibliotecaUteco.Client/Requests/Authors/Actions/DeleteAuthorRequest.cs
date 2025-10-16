using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Authors.Actions;

public class DeleteAuthorRequest
{
    [Range(1, int.MaxValue), Required]
    public int AuthorId { get; set; }
}