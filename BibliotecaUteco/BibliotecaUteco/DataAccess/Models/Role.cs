using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.DataAccess.Models;

public class Role : BaseEntity
{
    [MaxLength(25), MinLength(1)]
    public string Name { get; set; } = null!;
    public List<User> Users { get; set; } = new();
}