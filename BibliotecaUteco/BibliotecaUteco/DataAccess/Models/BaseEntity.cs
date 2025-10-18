using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUteco.DataAccess.Models;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Column("FechaCreacion")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("FechaActualizacion")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
