using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Loans.Actions;

public class CreateLoanRequest
{
    [Range(1, int.MaxValue), Required]
    public int ReaderId { get; set; }
    
    [MinLength(1), MaxLength(10), Required]
    public List<int> BookIds { get; set; } = new();

    [Range(1, 30), Required]
    public int MaxLoanDays { get; set; } = 1;
}