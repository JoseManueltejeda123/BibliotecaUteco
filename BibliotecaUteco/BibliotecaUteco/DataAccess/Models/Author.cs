using System.ComponentModel.DataAnnotations.Schema;
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

    public bool Update(UpdateAuthorCommand command)
    {

        var hasBeenUpdated = false;
        
        if(FullName != command.FullName)
        {
            FullName = command.FullName;
            NormalizedFullName = command.FullName.NormalizeField();
            hasBeenUpdated = true;
        }

        if (hasBeenUpdated)
        {
            UpdatedAt = DateTime.UtcNow;
        }

        return hasBeenUpdated;
    }

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