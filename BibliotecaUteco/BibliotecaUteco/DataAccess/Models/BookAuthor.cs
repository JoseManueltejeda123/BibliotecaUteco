namespace BibliotecaUteco.DataAccess.Models;

public class BookAuthor : BaseEntity
{
    public Author Author { get; set; } = null!;
    public int AuthorId { get; set; }
    public Book Book { get; set; } = null!;
    public int BookId { get; set; }
}