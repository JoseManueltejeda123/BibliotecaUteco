using BibliotecaUteco.Settings;

namespace BibliotecaUteco.DataAccess.Models;

public class Penalty : BaseEntity
{
    public int OverdueDays { get; set; }
    public Loan Loan { get; set; } = null!;
    public int LoanId { get; set; } 
    public bool IsDue { get; set; } = true;
    public double DailyFineRate { get; set; } = LoanSettings.DailyFineRate;
    public double TotalAmount {get; set; } 
    public double ReturnedAmount { get; set; }
    public double GivenAmount { get; set; }
    public Transaction Transaction { get; set; } = null!;
    public int TransactionId { get; set; }
}