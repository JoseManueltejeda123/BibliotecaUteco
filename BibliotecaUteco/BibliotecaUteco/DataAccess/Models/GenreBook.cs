namespace BibliotecaUteco.DataAccess.Models;

public class GenreBook : BaseEntity
{
    public Genre Genre { get; set; } = null!;
    public Book Book { get; set; } = null!;
    public int GenreId { get; set; }
    public int BookId { get; set; }
}