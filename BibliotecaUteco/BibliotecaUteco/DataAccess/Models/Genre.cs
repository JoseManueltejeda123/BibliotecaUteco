using System.ComponentModel.DataAnnotations;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Features.GenreFeatures.Actions;

namespace BibliotecaUteco.DataAccess.Models;

public class Genre : BaseEntity
{
    [MaxLength(25), MinLength(1), Required]
    public string Name { get; set; } = null!;

    [MaxLength(25), MinLength(1), Required]
    public string NormalizedName { get; set; } = null!;
    public List<GenreBook> Books { get; set; } = new();


    public static Genre Create(CreateGenreCommand command) => new()
    {
        Name = command.Name,
        NormalizedName = command.Name.Trim().ToLower().Normalize()
    };

    public GenreResponse ToResponse() => new()
    {
        Id = Id,
        Name = Name,
        CreatedAt = CreatedAt,
        UpdatedAt = UpdatedAt

    };
}