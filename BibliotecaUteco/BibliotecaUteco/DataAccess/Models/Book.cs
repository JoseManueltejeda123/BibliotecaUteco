using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Features.BooksFeatures.Actions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BibliotecaUteco.DataAccess.Models;

public class Book : BaseEntity
{
    [MaxLength(50), MinLength(1), Required]
    public string Name { get; set; } = null!;

    [MaxLength(50), MinLength(1), Required]
    public string NormalizedName { get; set; } = null!;
    
    [Url]
    public string? CoverUrl { get; set; }

    [MaxLength(500), MinLength(10), Required]
    public string Sinopsis { get; set; } = null!;
    
    public List<BookAuthor> Authors { get; set; } = new();
    public List<GenreBook> Genres { get; set; } = new();
    
    public List<BookLoan> Loans { get; set; } = new();

    public int Stock { get; set; }

    [NotMapped]
    public int AvailableAmount { get; set; }

    public static Book Create(CreateBookCommand command) => new()
    {
        Name = command.Name,
        NormalizedName = command.Name.ToLower().Trim().Normalize(),
        CoverUrl = command.CoverUrl,
        Sinopsis = command.Sinopsis,
        Stock = command.Stock,
        Genres = command.GenreIds.Select(g => new GenreBook(){GenreId = g}).ToList(),
        Authors = command.AuthorIds.Select(g => new BookAuthor(){AuthorId = g}).ToList(),
         
    };

    public BookResponse ToResponse() => new()
    {
        Id = Id,
        CreatedAt = CreatedAt,
        UpdatedAt = UpdatedAt,
        Name = Name,
        CoverUrl = CoverUrl ?? "",
        Sinopsis = Sinopsis,
        Authors = Authors.Select(a => a.ToResponse()).ToList(),
        Genres = Genres.Select(g => g.ToResponse()).ToList(),
        Stock = Stock,
        AvailableAmount = AvailableAmount


    };
    
}