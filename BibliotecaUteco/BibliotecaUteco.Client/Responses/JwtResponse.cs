namespace BibliotecaUteco.Client.Responses;

public class JwtResponse
{
    public required string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
    public required string Issuer {get; set;} 
}