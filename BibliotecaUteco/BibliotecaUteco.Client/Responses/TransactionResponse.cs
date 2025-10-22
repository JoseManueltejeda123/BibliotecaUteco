namespace BibliotecaUteco.Client.Responses;

public class TransactionResponse : BaseResponse
{
    
    public double Amount { get; set; }
    public UserResponse User { get; set; } = new();
    public int UserId { get; set; }
    
  
}