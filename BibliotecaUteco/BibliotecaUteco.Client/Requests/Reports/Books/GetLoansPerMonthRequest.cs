namespace BibliotecaUteco.Client.Requests.Reports.Books;

public class GetLoansPerMonthRequest
{
    public int Year { get; set; } = DateTime.Now.Year;
}