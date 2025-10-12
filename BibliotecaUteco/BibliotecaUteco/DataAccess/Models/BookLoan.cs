using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUteco.DataAccess.Models;

[Table("LibroPrestamo")]
public class BookLoan : BaseEntity
{
    public Book Book { get; set; } = null!;
    public Loan Loan { get; set; } = null!;
    
    [ Column("IdLibro")]

    public int BookId { get; set; }
    
    [ Column("IdPrestamos")]

    public int LoanId { get; set; } 
}