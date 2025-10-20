using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Loans.Queries;

public class GetLoansByFilterRequest
{
    [ MaxLength(11), MinLength(11)]
    public string? IdentityCardNumber { get; set; }

    [MaxLength(9), MinLength(3)]
   
    public string? StudentLicence { get; set; }


    public bool? JustPendingOnes { get; set; }

    public bool? JustExceededOnes { get; set; }


    public bool? JustReturnedOnes { get; set; }

    [Range(0, int.MaxValue)]
    public int? Skip { get; set; } = 0;

    [ Range(1, 100)]
    public int? Take { get; set; } = 5;
}