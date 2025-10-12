using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.Utilities;
using BibliotecaUteco.Features.AuthorFeatures.Actions;

namespace BibliotecaUteco.DataAccess.Models;

[Table("Autores")]
public class Author : BaseEntity
{
    
    
    [MaxLength(50), MinLength(1), Required, Column("NombreCompleto")]
    public string FullName { get; set; } = null!;

    [MaxLength(50), MinLength(1), Required,  Column("NombreCompletoNormalizado")]
    public string NormalizedFullName { get; set; } = null!;

    public List<BookAuthor> Books { get; set; } = new();

    [NotMapped]
    public int BooksCount { get; set; } = 0;


    public AuthorResponse ToResponse() => new()
    {
        Id = Id,
        UpdatedAt = UpdatedAt,
        CreatedAt = CreatedAt,
        BooksCount = BooksCount,
        FullName = FullName
    };

    public static Author Create(CreateAuthorCommand command) => new()
    {
        FullName = command.FullName.Trim(),
        NormalizedFullName = command.FullName.NormalizeField()
    };
}