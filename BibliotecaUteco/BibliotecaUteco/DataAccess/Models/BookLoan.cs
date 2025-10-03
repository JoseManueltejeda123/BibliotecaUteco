namespace BibliotecaUteco.DataAccess.Models;

public class BookLoan : BaseEntity
{
    public Book Book { get; set; } = null!;
    public Loan Loan { get; set; } = null!;
    public int BookId { get; set; }
    public int LoanId { get; set; } 
}