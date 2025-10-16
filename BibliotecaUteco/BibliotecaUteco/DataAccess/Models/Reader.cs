using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUteco.DataAccess.Models;

[Table("Lectores")]
public class Reader : BaseEntity
{
    [ Column("NombreCompleto")]

    [MaxLength(50), MinLength(5), Required]
    public string FullName { get; set; } = null!;
    
    [ Column("NumeroDeTelefono")]

    [MaxLength(10), MinLength(10), Required]
    public string PhoneNumber { get; set; } = null!;
    
    [ Column("Direccion")]

    [MaxLength(100), MinLength(30), Required]
    public string Address { get; set; } = null!;
    
    [ Column("Cedula")]

    [MaxLength(11), MinLength(11)] 
    public string IdentityCardNumber { get; set; } = null!;
    
    [ Column("Matricula")]

    [MaxLength(9), MinLength(3)]
    public string? StudentLicence { get; set; }
    
    public List<Loan> Loans { get; set; } = new();
    
}