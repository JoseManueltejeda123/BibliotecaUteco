namespace BibliotecaUteco.Client.Requests.Reports.Books;

public class GetTopBooksByDateRequest 
{
    
    public int Year {get; set;} = DateTime.Now.Year;
    public int Month { get; set; } = DateTime.Now.Month;
}