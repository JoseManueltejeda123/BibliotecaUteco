using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.DataAccess.Models;

public class Author : BaseEntity
{
    [MaxLength(50), MinLength(1), Required]
    public string FullName { get; set; } = null!;
    public List<BookAuthor> Books { get; set; } = new();
}