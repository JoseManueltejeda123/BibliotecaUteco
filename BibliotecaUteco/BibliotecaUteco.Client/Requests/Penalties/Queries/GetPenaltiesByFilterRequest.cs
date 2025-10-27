using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Penalties.Queries;

public class GetPenaltiesByFilterRequest
{
    public bool? IsDue { get; set; }

    public int LoanId { get; set; } = 0;

    [Range(1, 10, ErrorMessage = "El valor de 'take' debe estar entre 1 y 10.")]
    public int Take { get; set; } = 10;

    [Range(0, int.MaxValue, ErrorMessage = "El valor de 'skip' no puede ser negativo.")]
    public int Skip { get; set; } = 0;
}
