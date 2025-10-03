using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.DataAccess.Models;

public class Genre : BaseEntity
{
    [MaxLength(25), MinLength(1), Required]
    public string Name { get; set; } = null!;
    public List<GenreBook> Books { get; set; } = new();
}