using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUteco.DataAccess.Models;

[Table("Roles")]
public class Role : BaseEntity
{
    [ Column("Nombre")]
    [MaxLength(25), MinLength(1)]
    public string Name { get; set; } = null!;
    public List<User> Users { get; set; } = new();
}