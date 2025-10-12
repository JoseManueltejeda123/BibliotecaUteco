using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.Utilities;
using BibliotecaUteco.Features.BooksFeatures.Actions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BibliotecaUteco.DataAccess.Models;

[Table("Libros")]
public class Book : BaseEntity
{
    
    [MaxLength(50), MinLength(1), Required,  Column("Nombre")]
    public string Name { get; set; } = null!;

    [MaxLength(50), MinLength(1), Required,  Column("NombreNormalizado")]
    public string NormalizedName { get; set; } = null!;
    
    [Url,  Column("UrlPortada")]
    public string? CoverUrl { get; set; }

    [MaxLength(500), MinLength(10), Required,  Column("Synopsis")]
    public string Synopsis { get; set; } = null!;
    
    public List<BookAuthor> Authors { get; set; } = new();
    public List<GenreBook> Genres { get; set; } = new();
    
    public List<BookLoan> Loans { get; set; } = new();

    [Range(1, int.MaxValue),  Column("Copias")]
    public int Stock { get; set; }

    [NotMapped]
    public int AvailableAmount { get; set; }

    public static Book Create(CreateBookCommand command) => new()
    {
        Name = command.Name,
        NormalizedName = command.Name.NormalizeField(),
        CoverUrl = command.CoverUrl,
        Synopsis = command.Synopsis,
        Stock = command.Stock,
        Genres = command.GenreIds.Select(g => new GenreBook(){GenreId = g}).ToList(),
        Authors = command.AuthorIds.Select(g => new BookAuthor(){AuthorId = g}).ToList(),
         
    };


    public bool Update(UpdateBookCommand command)
    {
        bool hasBeenUpdated = false;

        if (Name != command.BookName)
        {
            Name = command.BookName;
            NormalizedName = command.BookName.NormalizeField();
            hasBeenUpdated = true;
        }
        
        if(Synopsis != command.Synopsis)
        {
            Synopsis = command.Synopsis;
            hasBeenUpdated = true;
        }

        if (Stock != command.Stock)
        {
            Stock = command.Stock;
            hasBeenUpdated = true;
        }
        
        if(hasBeenUpdated)
        {
            UpdatedAt = DateTime.UtcNow;
        }

        return hasBeenUpdated;
    }

    public BookResponse ToResponse() => new()
    {
        Id = Id,
        CreatedAt = CreatedAt,
        UpdatedAt = UpdatedAt,
        Name = Name,
        CoverUrl = CoverUrl ?? "",
        Synopsis = Synopsis,
        Authors = Authors.Select(a => a.ToResponse()).ToList(),
        Genres = Genres.Select(g => g.ToResponse()).ToList(),
        Stock = Stock,
        AvailableAmount = AvailableAmount


    };
    
}