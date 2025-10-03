using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUteco.DataAccess.Models;

public class Loan : BaseEntity
{
    [Range(7, 30)]
    public int MaxLoanDays { get; set; }
    public DateTime DueDate { get; set ; }
    public DateTime? ReturnedDate { get; set; } = null;
    public List<BookLoan> Books { get; set; } = new();
    public Reader Reader { get; set; } = null!;
    public int ReaderId { get; set; }
    public Penalty? Penalty { get; set; }

    [NotMapped] public int ExceededBy => ( DateTime.UtcNow - DueDate ).Days;
    [NotMapped] public bool IsExceeded => ExceededBy >= 1;
}