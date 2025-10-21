namespace BibliotecaUteco.Client.Responses;

public class LoanResponse : BaseResponse
{
    public int MaxLoanDays { get; set; }
    public DateTime DueDate { get; set; }

    public DateTime? ReturnedDate { get; set; } = null;

    public DateTime DueDateLocal => DueDate.ToLocalTime();
    public string DueDateLocalFormatted => DueDateLocal.ToString("dd MMM yyyy hh:mm tt", new System.Globalization.CultureInfo("es-ES"));
    public DateTime? ReturnedDateLocal => ReturnedDate?.ToLocalTime();
    public string? ReturnedDateLocalFormatted => ReturnedDateLocal?.ToString("dd MMM yyyy hh:mm tt", new System.Globalization.CultureInfo("es-ES")) ;

    public ReaderResponse Reader { get; set; } = null!;

    public List<BookResponse> Books { get; set; } = new();

    public int ReaderId { get; set; }
    
    public bool HasPenalty { get; set; }
    
    public int BookCount { get; set; }
    public int ExceededBy { get; set; }

    public bool IsExceeded { get; set; }
}