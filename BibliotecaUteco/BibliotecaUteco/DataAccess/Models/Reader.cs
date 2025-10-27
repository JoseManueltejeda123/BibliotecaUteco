using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaUteco.Features.ReadersFeatures.Actions;
using BibliotecaUteco.Features.UserFeatures.Actions;

namespace BibliotecaUteco.DataAccess.Models;

[Table("Lectores")]
public class Reader : BaseEntity
{
    [Column("NombreCompleto")]
    [MaxLength(50), MinLength(5), Required]
    public string FullName { get; set; } = null!;

    [Column("NumeroDeTelefono")]
    [MaxLength(10), MinLength(10), Required]
    public string PhoneNumber { get; set; } = null!;

    [Column("Direccion")]
    [MaxLength(100), MinLength(10), Required]
    public string Address { get; set; } = null!;

    [Column("Cedula")]
    [MaxLength(11), MinLength(11)]
    public string IdentityCardNumber { get; set; } = null!;

    [Column("Matricula")]
    [MaxLength(9), MinLength(3)]
    public string? StudentLicence { get; set; }

    public List<Loan> Loans { get; set; } = new();

    [NotMapped]
    public int LoansCount { get; set; } = 0;

    [NotMapped]
    public int ReturnedLoans { get; set; } = 0;

    [NotMapped]
    public DateTime? LastLoanDate { get; set; }

    [NotMapped]
    public bool LastLoanIsActive { get; set; }

    [Column("IdSexo")]
    public int? SexId { get; set; }

    public Sex? Sex { get; set; }

    public bool Update(UpdateReaderCommand command)
    {
        bool hasBeenUpdated = false;

        if (FullName != command.FullName)
        {
            FullName = command.FullName.Trim();
            hasBeenUpdated = true;
        }

        if (PhoneNumber != command.PhoneNumber)
        {
            PhoneNumber = command.PhoneNumber.Trim();
            hasBeenUpdated = true;
        }

        if (Address != command.Address)
        {
            Address = command.Address.Trim();
            hasBeenUpdated = true;
        }

        if (IdentityCardNumber != command.IdentityCardNumber)
        {
            IdentityCardNumber = command.IdentityCardNumber.Trim();
            hasBeenUpdated = true;
        }

        if (SexId != command.SexId)
        {
            SexId = command.SexId;
            hasBeenUpdated = true;
        }

        // Si la matrícula cambia o se elimina

        if (StudentLicence != command.StudentLicence)
        {
            StudentLicence = command.StudentLicence;
            hasBeenUpdated = true;
        }

        if (hasBeenUpdated)
        {
            UpdatedAt = DateTime.UtcNow;
        }

        return hasBeenUpdated;
    }

    public static Reader Create(CreateReaderCommand request) =>
        new()
        {
            FullName = request.FullName.Trim(),
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            SexId = request.SexId,
            IdentityCardNumber = request.IdentityCardNumber,
            StudentLicence = !string.IsNullOrWhiteSpace(request.StudentLicence)
                ? request.StudentLicence.ToUpper().Trim()
                : null,
        };

    public ReaderResponse ToResponse() =>
        new()
        {
            Id = Id,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            FullName = FullName,
            PhoneNumber = PhoneNumber,
            Address = Address,
            StudentLicence = StudentLicence,
            IdentityCardNumber = IdentityCardNumber,
            LoansCount = LoansCount,
            LastLoanDate = LastLoanDate,
            LastLoanIsActive = LastLoanIsActive,
            ReturnedLoansCount = ReturnedLoans,
            SexId = SexId ?? 1,
        };
}
