namespace BibliotecaUteco.Client.Responses;

public class BaseResponse
{
    public int Id {get; set;}
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAtLocal => CreatedAt.ToLocalTime();
    public DateTime UpdatedAtLocal => UpdatedAt.ToLocalTime();
    
    public string UpdatedAtLocalFormatted =>  UpdatedAtLocal.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("es-ES"));
    public string CreatedAtLocalFormatted =>  CreatedAt.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("es-ES"));

}