using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaUteco.Features.LoansFeatures.Actions;

namespace BibliotecaUteco.DataAccess.Models;

[Table("Prestamos")]
public class Loan : BaseEntity
{
    [Column("MaxDiasDePrestamo")]
    [Range(7, 30)]
    public int MaxLoanDays { get; set; }

    [Column("FechaEntrega")]
    public DateTime DueDate { get; set; } 

    [Column("FechaDevolucion")]
    public DateTime? ReturnedDate { get; set; } = null;
    public List<BookLoan> Books { get; set; } = new();

    public Reader Reader { get; set; } = null!;

    [Column("IdLector")]
    public int ReaderId { get; set; }
    public Penalty? Penalty { get; set; }

    [NotMapped]
    public int ExceededBy => (DateTime.UtcNow - DueDate).Days;

    [NotMapped]
    public bool IsExceeded => ExceededBy >= 1;
    
    [NotMapped]
    public bool HasPenalty { get; set; }
    
    [NotMapped]
    public int BookCount { get; set; }
    
    public static Loan Create(CreateLoanCommand command) => new()
    {
      MaxLoanDays  = command.MaxLoanDays,
      ReaderId = command.ReaderId,
      Books = command.BookIds.Select(b => new BookLoan(){BookId = b}).ToList(),
      DueDate = DateTime.UtcNow.AddDays(command.MaxLoanDays),
      
    };

    public LoanResponse ToResponse() => new()
    {
        Id = Id,
        CreatedAt = CreatedAt,
        UpdatedAt = CreatedAt,
        MaxLoanDays = MaxLoanDays,
        DueDate = DueDate,
        ReturnedDate = ReturnedDate,
        Reader = Reader.ToResponse() ?? new(),
        ReaderId = ReaderId,
        HasPenalty = HasPenalty,
        BookCount = BookCount
    };
}
