using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaUteco.Client.Settings;
using BibliotecaUteco.Settings;

namespace BibliotecaUteco.DataAccess.Models;

[Table("Penalizaciones")]
public class Penalty : BaseEntity
{
    [ Column("DiasExcedidos")]
    public int OverdueDays { get; set; }
    public Loan Loan { get; set; } = null!;
    [ Column("IdPrestamo")]
    public int LoanId { get; set; } 
    
    [ Column("EsDebida")]
    public bool IsDue { get; set; } = true;
    
    [ Column("TazaDeMultaPorDia")]
    public double DailyFineRate { get; set; } = LoanSettings.DailyFineRate;
    
    [ Column("TotalAPagar")]
    
    public double TotalAmount {get; set; } 
    
    [ Column("MontoDevuelto")]
    public double ReturnedAmount { get; set; }
    
    [ Column("MontoDado")]
    public double GivenAmount { get; set; }
    public Transaction Transaction { get; set; } = null!;
    
    [ Column("IdTransaccion")]
    public int TransactionId { get; set; }
}