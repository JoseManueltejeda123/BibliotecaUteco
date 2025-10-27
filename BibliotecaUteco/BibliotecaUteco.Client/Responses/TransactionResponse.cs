namespace BibliotecaUteco.Client.Responses;

public class TransactionResponse : BaseResponse
{
    public double Amount { get; set; }
    public UserResponse User { get; set; } = new();
    public int UserId { get; set; }
}

public class CashBoxSummaryResponse
{
    public double? CashBoxState { get; set; }
    public DateTime? LastDepositDate { get; set; }
    public double LastDepositAmount { get; set; }
    public DateTime? LastRetirementDate { get; set; }
    public double LastRetirementAmount { get; set; }

    public DateTime _lastDepositDate => LastDepositDate?.ToLocalTime() ?? DateTime.Now;

    public DateTime _lastRetirementDate => LastRetirementDate?.ToLocalTime() ?? DateTime.Now;

    public string _lastRetirementDateFormatted =>
        _lastRetirementDate.ToString(
            "dd MMM yyyy hh:mm tt",
            new System.Globalization.CultureInfo("es-ES")
        );
    public string _lastDepositDateFormatted =>
        _lastDepositDate.ToString(
            "dd MMM yyyy hh:mm tt",
            new System.Globalization.CultureInfo("es-ES")
        );
}
