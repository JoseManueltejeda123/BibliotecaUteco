using System.Text.RegularExpressions;
using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Responses;

public class ReaderResponse : BaseResponse
{
    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string IdentityCardNumber { get; set; } = null!;

    public string? StudentLicence { get; set; }

    public int LoansCount { get; set; } = 0;
    public DateTime? LastLoanDate { get; set; }
    public bool LastLoanIsActive { get; set; }
    public int SexId { get; set; }
    public ApplicationSexes Sex {get; set;} 
        public int ReturnedLoansCount { get; set; } = 0;


    public string FormattedPhoneNumber => !string.IsNullOrEmpty(PhoneNumber) ?
        Regex.Replace(PhoneNumber, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3") : "";
}
