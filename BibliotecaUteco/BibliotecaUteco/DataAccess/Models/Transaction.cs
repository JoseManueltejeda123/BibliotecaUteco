namespace BibliotecaUteco.DataAccess.Models;

public class Transaction : BaseEntity
{
    public double Amount { get; set; }
    public User User { get; set; } = null!;
    public int UserId { get; set; }
    public Penalty? Penalty { get; set; }
}