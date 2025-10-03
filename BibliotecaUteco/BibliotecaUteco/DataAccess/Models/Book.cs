using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.DataAccess.Models;

public class Book : BaseEntity
{
    [MaxLength(100), MinLength(1), Required]
    public string Name { get; set; } = null!;
    
    [Url]
    public string? CoverUrl { get; set; }
    
    [MaxLength(500), MinLength(10), Required]
    public string Description { get; set; } = null!;
    
    public List<BookAuthor> Authors { get; set; } = new();
    public List<GenreBook> Genres { get; set; } = new();
    
    public List<BookLoan> Loans { get; set; } = new();

    public int Stock { get; set; }
    
}