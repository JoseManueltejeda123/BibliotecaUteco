namespace BibliotecaUteco.Client.Responses;

public class PenaltyResponse : BaseResponse
{
    public int OverdueDays { get; set; }

    public int LoanId { get; set; }

    public bool IsDue { get; set; } = true;

    public double DailyFineRate { get; set; }
    public string _dailyFineRate => DailyFineRate.ToString("F2");

    public double TotalAmount { get; set; }
    public string _totalAmount => TotalAmount.ToString("F2");
    public double ReturnedAmount { get; set; }
    public string _returnedAmount => ReturnedAmount.ToString("F2");

    public double GivenAmount { get; set; }
    public string _givenAmount => GivenAmount.ToString("F2");

    public int? TransactionId { get; set; }

    public int ReaderId { get; set; }
    public string ReaderIdentityCardNumber { get; set; } = "";
    public string ReaderName { get; set; } = "";
}
