using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaUteco.Features.GenreFeatures.Actions;

namespace BibliotecaUteco.DataAccess.Models;

[Table("Genero")]
public class Genre : BaseEntity
{
    [MaxLength(25), MinLength(1), Required, Column("Nombre")]
    public string Name { get; set; } = null!;

    [MaxLength(25), MinLength(1), Required, Column("NombreNormalizado")]
    public string NormalizedName { get; set; } = null!;
    public List<GenreBook> Books { get; set; } = new();


    public static Genre Create(CreateGenreCommand command) => new()
    {
        Name = command.Name,
        NormalizedName = command.Name.NormalizeField()
    };

    public bool Update(UpdateGenreCommand command)
    {
        bool hasBeenUpdated = false;

        if(Name != command.GenreName)
        {
            Name = command.GenreName;
            NormalizedName = command.GenreName.NormalizeField();

            hasBeenUpdated = true;
        }

        if (hasBeenUpdated)
        {
            UpdatedAt = DateTime.UtcNow;
        }

        return hasBeenUpdated;
    }

    public GenreResponse ToResponse() => new()
    {
        Id = Id,
        Name = Name,
        CreatedAt = CreatedAt,
        UpdatedAt = UpdatedAt

    };
}