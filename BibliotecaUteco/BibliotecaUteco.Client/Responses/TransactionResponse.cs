namespace BibliotecaUteco.Client.Responses;

public class TransactionResponse : BaseResponse
{
    
    public double Amount { get; set; }
    public UserResponse User { get; set; } = new();
    public int UserId { get; set; }
    
  
}

public class CashBoxSummary
{
    public double CashBoxState {get; set;}
    public DateTime LastDepositDate {get; set;}
    public double LastDepositAmount {get; set;}
    public DateTime LastRetirementDate { get; set; }
    public DateTime LastRetirementAmount { get; set; }

}