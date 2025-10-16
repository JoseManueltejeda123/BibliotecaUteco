using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Authors.Actions;

public class UpdateAuthorRequest
{
    [Range(1, int.MaxValue)]
    public int AuthorId { get; set; }

    [MaxLength(50), MinLength(1)]
    public string FullName { get; set; } = null!;
}