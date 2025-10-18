using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Readers.Queries;

public class GetReadersByFilterRequest
{
   
    [MaxLength(11, ErrorMessage = "La cédula no puede tener más de 11 caracteres.")]
    [MinLength(1, ErrorMessage = "La cédula debe de tener almenos 1 caracter.")]
    public string? IdentityCardNumber { get; set; }

    [MaxLength(9, ErrorMessage = "La matricula no puede tener más de 9 caracteres.")]
    [MinLength(1, ErrorMessage = "La matricula debe tener al menos 1 caracteres.")]
    public string? StudentLicence { get; set; }
    
    [Range(0, int.MaxValue)]
    public int Skip { get; set; }
    
    [ Range(1, 15)]
    public int Take { get; set; }
}