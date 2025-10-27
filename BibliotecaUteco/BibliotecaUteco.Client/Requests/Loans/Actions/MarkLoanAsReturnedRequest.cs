using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Loans.Actions;

public class MarkLoanAsReturnedRequest
{
    [Required, Range(1, int.MaxValue)]
    public int LoanId { get; set; }
}
