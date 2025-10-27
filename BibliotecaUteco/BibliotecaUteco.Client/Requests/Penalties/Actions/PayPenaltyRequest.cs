using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Penalties.Actions;

public class PayPenaltyRequest
{
    [Required(ErrorMessage = "El campo 'Id' es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El 'Id' debe ser mayor o igual a 1.")]
    public int PenaltyId { get; set; }

    [Required(ErrorMessage = "El campo 'GivenAmount' es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El 'GivenAmount' debe ser mayor a 0.")]
    public double GivenAmount { get; set; }
}
