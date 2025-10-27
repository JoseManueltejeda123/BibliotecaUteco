using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUteco.DataAccess.Models;

[Table("TransaccionesCaja")]
public class Transaction : BaseEntity
{
    [Column("Monto")]
    public double Amount { get; set; }
    public User User { get; set; } = null!;

    [Column("IdUsuario")]
    public int UserId { get; set; }
    public Penalty? Penalty { get; set; }

    public static Transaction Create(int userId, double amount) =>
        new() { Amount = amount, UserId = userId };

    public TransactionResponse ToResponse() =>
        new()
        {
            Id = Id,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            Amount = Amount,
            User = User.ToResponse(),
            UserId = UserId,
        };
}
