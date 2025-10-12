using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUteco.DataAccess.Models;

public abstract class BaseEntity
{
    [Key]
    public int Id {get; set;}
    
    [Column("CreadoEn")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("ActualizadoEn")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}