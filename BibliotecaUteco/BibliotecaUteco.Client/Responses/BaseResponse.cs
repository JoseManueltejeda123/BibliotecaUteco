namespace BibliotecaUteco.Client.Responses;

public class BaseResponse
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAtLocal => CreatedAt.ToLocalTime();
    public DateTime UpdatedAtLocal => UpdatedAt.ToLocalTime();

    public string UpdatedAtLocalFormatted =>
        UpdatedAtLocal.ToString("dd MMM yyyy hh:mm tt", new System.Globalization.CultureInfo("es-ES"));
    public string CreatedAtLocalFormatted =>
        CreatedAtLocal.ToString("dd MMM yyyy hh:mm tt", new System.Globalization.CultureInfo("es-ES"));
}
