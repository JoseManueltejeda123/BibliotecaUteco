using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.DataAccess.Models;

[Table("GeneroLibro")]
public class GenreBook : BaseEntity
{
    public Genre Genre { get; set; } = null!;
    public Book Book { get; set; } = null!;
    
    [ Column("IdGenero")]

    public int GenreId { get; set; }
    
    [Column("IdLibro")]

    public int BookId { get; set; }

    public GenreBookResponse ToResponse() => new()
    {
        Id = Id,
        CreatedAt = CreatedAt,
        UpdatedAt = UpdatedAt,
        BookId = BookId,
        GenreId = GenreId,
        Genre = Genre.ToResponse()

    };
}