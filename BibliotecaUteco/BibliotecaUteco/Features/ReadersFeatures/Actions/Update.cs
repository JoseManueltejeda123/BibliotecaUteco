using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Features.ReadersFeatures.Actions
{
    public class UpdateReaderCommand : ICommand<IApiResult>
    {

        [FromBody, JsonPropertyName("readerId"), Range(1, int.MaxValue)]
        [Description("ID único del lector.")]
        public int ReaderId { get; set; }

        [FromBody, JsonPropertyName("fullName"), Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El nombre completo no puede tener más de 50 caracteres.")]
        [MinLength(5, ErrorMessage = "El nombre completo debe tener al menos 5 caracteres.")]
        [Description("Nombre completo del lector.")]
        public string FullName { get; set; } = null!;

        [FromBody, JsonPropertyName("phoneNumber"), Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El número de teléfono debe tener exactamente 10 dígitos.")]
        [MinLength(10, ErrorMessage = "El número de teléfono debe tener exactamente 10 dígitos.")]
        [Description("Número telefónico del lector (10 dígitos, debe iniciar con 809, 829 o 849).")]
        public string PhoneNumber { get; set; } = null!;

        [FromBody, JsonPropertyName("address"), Required(ErrorMessage = "La dirección es obligatoria.")]
        [MaxLength(100, ErrorMessage = "La dirección no puede superar los 100 caracteres.")]
        [MinLength(10, ErrorMessage = "La dirección debe tener al menos 10 caracteres.")]
        [Description("Dirección completa del lector.")]
        public string Address { get; set; } = null!;

        [FromBody, JsonPropertyName("identityCardNumber"), Required(ErrorMessage = "El número de cédula es obligatorio.")]
        [MaxLength(11, ErrorMessage = "El número de cédula debe tener exactamente 11 dígitos.")]
        [MinLength(11, ErrorMessage = "El número de cédula debe tener exactamente 11 dígitos.")]
        [Description("Cédula de identidad del lector (11 dígitos sin guiones).")]
        public string IdentityCardNumber { get; set; } = null!;

        [FromBody, JsonPropertyName("sexId"), Range(1, 2)]
        [Description("ID del sexo del lector (1 = Masculino, 2 = Femenino).")]
        public int SexId { get; set; } = 1;

        [FromBody, JsonPropertyName("studentLicence")]
        [MaxLength(9, ErrorMessage = "La matrícula estudiantil no puede tener más de 9 caracteres.")]
        [MinLength(3, ErrorMessage = "La matrícula estudiantil debe tener al menos 3 caracteres.")]
        [Description("Número de matrícula del estudiante (opcional).")]
        public string? StudentLicence { get; set; }


    }
    public class UpdateReaderCommandValidator : AbstractValidator<UpdateReaderCommand>
    {
        public UpdateReaderCommandValidator()
        {
            RuleFor(x => x.ReaderId)
                .GreaterThan(0)
                .WithMessage("Debe proporcionar un ID de lector válido.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("El nombre completo es obligatorio.")
                .MinimumLength(5).WithMessage("El nombre debe tener al menos 5 caracteres.")
                .MaximumLength(50).WithMessage("El nombre no puede superar los 50 caracteres.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("El número de teléfono es obligatorio.")
                .Matches(@"^\d{10}$").WithMessage("El número telefónico debe tener exactamente 10 dígitos numéricos.")
                .Must(x => x.StartsWith("809") || x.StartsWith("829") || x.StartsWith("849"))
                .WithMessage("El número de teléfono debe comenzar con 809, 829 o 849.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("La dirección es obligatoria.")
                .MinimumLength(10).WithMessage("La dirección debe tener al menos 10 caracteres.")
                .MaximumLength(100).WithMessage("La dirección no puede superar los 100 caracteres.");

            RuleFor(x => x.IdentityCardNumber)
                .NotEmpty().WithMessage("La cédula es obligatoria.")
                .Matches(@"^\d{11}$").WithMessage("La cédula solo puede contener números y tener 11 dígitos.");

            When(x => !string.IsNullOrWhiteSpace(x.StudentLicence), () =>
            {
                RuleFor(x => x.StudentLicence)
                    .MinimumLength(3)
                    .MaximumLength(9)
                    .Matches(@"^[0-9\-]+$")
                    .WithMessage("La matrícula solo puede contener números y guiones.");
            });
        }
    }

    internal class UpdateReaderEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut(
                    EndpointSettings.ReadersEndpoint,
                    async (
                        
                        [FromBody] UpdateReaderCommand command,
                        ISender sender,
                        IEndpointWrapper<UpdateReaderEndpoint> wrapper,
                        CancellationToken cancellationToken = default
                    ) =>
                    {

                        return await wrapper.ExecuteAsync<IApiResult>(async () =>
                        {
                            return await sender.SendAndValidateAsync(command, cancellationToken);
                        });
                    }
                )
                .RequireAuthorization(AuthorizationPolicies.AllowAuthorizedUsers)
                .RequireCors(CorsPolicies.DefaultPolicy)
                .DisableAntiforgery()
                .Accepts<UpdateReaderCommand>(false, ApplicationContentTypes.ApplicationJson)
                .Produces<SuccessApiResult<ReaderResponse>>(200, ApplicationContentTypes.ApplicationJson)
                .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)                .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)                .ProducesProblem(409, ApplicationContentTypes.ApplicationJson)
                                .Produces<InternalServerErrorApiResult>(500, ApplicationContentTypes.ApplicationJson)
                                .Produces<ForbiddenApiResult>(500, ApplicationContentTypes.ApplicationJson)
                .WithTags(nameof(Reader))
                .WithName(nameof(UpdateReaderEndpoint))
                .WithDescription("Actualiza la información de un lector existente en el sistema");
        }
    }

public class UpdateReaderCommandHandler(IBibliotecaUtecoDbContext context) : ICommandHandler<UpdateReaderCommand, IApiResult>
{

    public async Task<IApiResult> HandleAsync(UpdateReaderCommand request, CancellationToken cancellationToken = default)
    {
        if(await context.Readers.FirstOrDefaultAsync(x => x.Id == request.ReaderId, cancellationToken) is var reader && reader is null)
        {
            return new NotFoundApiResult( "No se encontró el lector especificado.");
        }

        if (await context.Readers.AnyAsync(r => r.IdentityCardNumber == request.IdentityCardNumber  && r.Id != request.ReaderId, cancellationToken))
        {
            return new ConflictApiResult( "Ya existe un lector con esa cédula.");
        }

        if (!string.IsNullOrWhiteSpace(request.StudentLicence))
        {
            if (await context.Readers.AnyAsync(r => r.StudentLicence == request.StudentLicence && r.Id != request.ReaderId, cancellationToken))
            {
                return new ConflictApiResult( "Ya existe un lector con esa matrícula.");
            }
        }
        

        if (await context.Readers.AnyAsync(r => r.PhoneNumber == request.PhoneNumber && r.Id != request.ReaderId, cancellationToken))
        {
            return new ConflictApiResult( "Ya existe un lector con ese número de teléfono.");
        }

        reader.Update(request);       
        await context.SaveChangesAsync(cancellationToken);
        context.ChangeTracker.Clear();

        var updatedReader = await context.Readers.GetByIdAsync(request.ReaderId, cancellationToken);
        if (updatedReader is null)
        {
            return new BadRequestApiResult( "Error al actualizar el lector.");
        }

        return new SuccessApiResult<ReaderResponse>(updatedReader.ToResponse());
    }
}


}


