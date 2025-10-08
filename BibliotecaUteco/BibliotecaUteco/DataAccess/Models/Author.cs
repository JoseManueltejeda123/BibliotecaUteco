using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.DataAccess.Models;

public class Author : BaseEntity
{
    [MaxLength(50), MinLength(1), Required]
    public string FullName { get; set; } = null!;

    [MaxLength(50), MinLength(1), Required]
    public string NormalizedFullName { get; set; } = null!;

    public List<BookAuthor> Books { get; set; } = new();

    [NotMapped]
    public int BooksCount { get; set; } = 0;


    public AuthorResponse ToResponse() => new()
    {
        Id = Id,
        UpdatedAt = UpdatedAt,
        CreatedAt = CreatedAt,
        BooksCount = BooksCount
    };
}