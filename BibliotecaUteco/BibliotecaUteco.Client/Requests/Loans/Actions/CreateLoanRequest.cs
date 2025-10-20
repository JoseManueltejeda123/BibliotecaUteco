using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Loans.Actions;

public class CreateLoanRequest
{
    [Range(1, int.MaxValue), Required]
    public int ReaderId { get; set; }
    
    [MinLength(1), MaxLength(10), Required]
    public List<int> BookIds { get; set; } = new();

    [Range(7, 14), Required]
    public int MaxLoanDays { get; set; } = 14;
}