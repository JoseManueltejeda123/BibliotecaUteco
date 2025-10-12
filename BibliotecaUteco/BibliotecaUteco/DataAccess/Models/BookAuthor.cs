using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.DataAccess.Models;

[Table("LibroAutor")]
public class BookAuthor : BaseEntity
{
    public Author Author { get; set; } = null!;
    
    [ Column("IdAutor")]
    public int AuthorId { get; set; }
    
    public Book Book { get; set; } = null!;
    
    [ Column("IdLibro")]
    public int BookId { get; set; }


    public BookAuthorResponse ToResponse() => new()
    {
        Id = Id,
        CreatedAt = CreatedAt,
        UpdatedAt = UpdatedAt,
        BookId = BookId,
        AuthorId = AuthorId,
        Author = Author.ToResponse()

    };
}
