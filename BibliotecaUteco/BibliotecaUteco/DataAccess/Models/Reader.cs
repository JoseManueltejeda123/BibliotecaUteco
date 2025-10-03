using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.DataAccess.Models;

public class Reader : BaseEntity
{
    [MaxLength(50), MinLength(5), Required]
    public string FullName { get; set; } = null!;
    
    [MaxLength(10), MinLength(10), Required]
    public string PhoneNumber { get; set; } = null!;
    
    [MaxLength(100), MinLength(30), Required]
    public string Address { get; set; } = null!;
    
    [MaxLength(11), MinLength(11)] 
    public string IdentityCardNumber { get; set; } = null!;
    
    [MaxLength(9), MinLength(3)]
    public string? StudentLicence { get; set; }
    
    public List<Loan> Loans { get; set; } = new();
    
}