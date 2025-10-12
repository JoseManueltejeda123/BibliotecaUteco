using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.DataAccess.Models;

[Table("Usuarios")]
public class User : BaseEntity
{
    [ Column("NombreCompleto")]
    [MaxLength(50), MinLength(5), Required]
    public string FullName { get; set; } = null!;
    
    
    [ Column("Clave")]
    [MinLength(8), Required] 
    //No max lenght por que esto va hasheado
    public string Password { get; set; } = null!;
    
    [ Column("UrlFotoDePerfil")]
    public string? ProfilePictureUrl { get; set; } 
    
    [ Column("NombreDeUsuario")]
    [MaxLength(15), MinLength(5), RegularExpression(@"^[a-zA-Z0-9._]+$")]
    public string Username { get; set; } = null!;

    [ Column("Cedula")]
    [MaxLength(11), MinLength(11)] 
    public string IdentityCardNumber { get; set; } = null!;
    
    public Role Role { get; set; } = null!;

    [ Column("IdRole")]
    public int RoleId { get; set; } = 0;
    
    public List<Transaction> Transactions { get; set; } = new();

    public UserResponse ToResponse() => new()
    {
        Id = Id,
        CreatedAt = CreatedAt,
        UpdatedAt = UpdatedAt,
        UserName = Username,
        FullName = FullName,
        IdentityCardNumber = IdentityCardNumber,
        ProfilePictureUrl = ProfilePictureUrl ?? "",
        RoleName = Role.Name,
        RoleId = RoleId
    };

}