using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaUteco.Client.Settings;

namespace BibliotecaUteco.DataAccess.Models;

[Table("Penalizaciones")]
public class Penalty : BaseEntity
{
    [Column("DiasExcedidos")]
    public int OverdueDays { get; set; }
    public Loan Loan { get; set; } = null!;

    [Column("IdPrestamo")]
    public int LoanId { get; set; }

    [Column("EsDebida")]
    public bool IsDue { get; set; } = true;

    [Column("TazaDeMultaPorDia")]
    public double DailyFineRate { get; set; } = LoanSettings.DailyFineRate;

    [Column("TotalAPagar")]
    public double TotalAmount { get; set; }

    [Column("MontoDevuelto")]
    public double ReturnedAmount { get; set; }

    [Column("MontoDado")]
    public double GivenAmount { get; set; }
    public Transaction? Transaction { get; set; }

    [Column("IdTransaccion")]
    public int? TransactionId { get; set; }

    [NotMapped]
    public int ReaderId { get; set; }

    [NotMapped]
    public string ReaderIdentityCardNumber { get; set; } = "";

    [NotMapped]
    public string ReaderName { get; set; } = "";

      [NotMapped]
    public string? ReaderStudentLicence { get; set; } = "";

    public bool Pay(double givenAmount, int transactionId)
    {
        if (givenAmount < TotalAmount)
            return false;
        if (transactionId == 0)
            return false;

        GivenAmount = givenAmount;
        ReturnedAmount = GivenAmount - TotalAmount;
        TransactionId = transactionId;
        IsDue = false;
        UpdatedAt = DateTime.UtcNow;

        return true;
    }

    public static Penalty Create(Loan loan)
    {
        var overdueDays = (DateTime.UtcNow.Date - loan.DueDate.Date).Days;
        var totalAmount = overdueDays * LoanSettings.DailyFineRate;

        return new Penalty
        {
            LoanId = loan.Id,
            Loan = loan,
            OverdueDays = overdueDays,
            IsDue = true,
            DailyFineRate = LoanSettings.DailyFineRate,
            TotalAmount = totalAmount,
            ReturnedAmount = 0,
            GivenAmount = 0,
        };
    }

    public PenaltyResponse ToResponse() =>
        new()
        {
            Id = Id,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            OverdueDays = OverdueDays,
            LoanId = LoanId,
            IsDue = IsDue,
            DailyFineRate = DailyFineRate,
            TotalAmount = TotalAmount,
            ReturnedAmount = ReturnedAmount,
            GivenAmount = GivenAmount,
            TransactionId = TransactionId,
            ReaderId = ReaderId,
            ReaderIdentityCardNumber = ReaderIdentityCardNumber ?? "",
            ReaderStudentLicence = ReaderStudentLicence ?? "",
            ReaderName = ReaderName ?? "",
        };
}
