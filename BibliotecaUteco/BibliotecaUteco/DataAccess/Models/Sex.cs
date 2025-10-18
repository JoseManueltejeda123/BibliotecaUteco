using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUteco.DataAccess.Models;

[Table("Sexos")]
public class Sex : BaseEntity
{
    [Column("Nombre"), MaxLength(15), MinLength(1)]
    public string Name { get; set; } = null!;
    public List<Reader> Readers { get; set; } = new List<Reader>();
    public List<User> Users { get; set; } = new List<User>();
}