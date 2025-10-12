using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUteco.DataAccess.Models;

[Table("Prestamos")]
public class Loan : BaseEntity
{
    [ Column("MaxDiasDePrestamo")]
    [Range(7, 30), ]
    public int MaxLoanDays { get; set; }
    
    [ Column("DiaDeEntrega")]
    public DateTime DueDate { get; set ; }
    
    [ Column("DiaDevuelto")]
    public DateTime? ReturnedDate { get; set; } = null;
    public List<BookLoan> Books { get; set; } = new();
    
    public Reader Reader { get; set; } = null!;
    
    [ Column("IdLector")]
    public int ReaderId { get; set; }
    public Penalty? Penalty { get; set; }

    [NotMapped] public int ExceededBy => ( DateTime.UtcNow - DueDate ).Days;
    [NotMapped] public bool IsExceeded => ExceededBy >= 1;
}