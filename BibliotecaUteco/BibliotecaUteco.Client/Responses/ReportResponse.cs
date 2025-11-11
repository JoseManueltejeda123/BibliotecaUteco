namespace BibliotecaUteco.Client.Responses;

public class TopBooksResponse
{
    public int Month {get; set;}
    public int Year {get; set;}
    public List<BookResponse> Books { get; set; } = new();
}

public class LoansPerMonthResponse
{

    public int Year { get; set; }
    public Dictionary<string, int> MonthLoanCount { get; set; } = new();
}
