namespace BibliotecaUteco.Features.ReadersFeatures.Actions;

 public class CreateReaderCommand : ICommand<IApiResult>
    {
        [FromBody, JsonPropertyName("fullName"), Required, MaxLength(50), MinLength(5)]
        [Description("Nombre completo del lector")]
        public string FullName { get; set; } = null!;

        [FromBody, JsonPropertyName("phoneNumber"), Required, MaxLength(10), MinLength(10)]
        [Description("Número de teléfono (10 dígitos)")]
        public string PhoneNumber { get; set; } = null!;

        [FromBody, JsonPropertyName("address"), Required, MaxLength(100), MinLength(10)]
        [Description("Dirección completa")]
        public string Address { get; set; } = null!;

        [FromBody, JsonPropertyName("identityCardNumber"), Required, MaxLength(11), MinLength(11)]
        [Description("Cédula de identidad (11 dígitos)")]
        public string IdentityCardNumber { get; set; } = null!;
        
        [FromBody, JsonPropertyName("sexId"), Required, Range(1, 2)]
        [Description("ID del sexo")]
        public int SexId { get; set; }

        [FromBody, JsonPropertyName("studentLicence"), MaxLength(9), MinLength(3)]
        [Description("Matrícula de estudiante (opcional)")]
        public string? StudentLicence { get; set; }
    }



 public class CreateReaderCommandValidator : AbstractValidator<CreateReaderCommand>
 {
     public CreateReaderCommandValidator()
     {
         RuleFor(x => x.FullName)
             .NotEmpty().WithMessage("El nombre completo es obligatorio")
             .MinimumLength(5).WithMessage("El nombre debe tener al menos 5 caracteres")
             .MaximumLength(50).WithMessage("El nombre no puede superar los 50 caracteres");

         RuleFor(x => x.PhoneNumber)
             .NotEmpty().WithMessage("El número de teléfono es obligatorio")
             .Length(10).WithMessage("El número de teléfono debe tener exactamente 10 dígitos")
             .Matches(@"^\d{10}$").WithMessage("El número de teléfono solo puede contener números")
             .Must(phone => phone.StartsWith("809") || phone.StartsWith("829") || phone.StartsWith("849"))
             .WithMessage("El número debe comenzar con 809, 829 o 849");

         RuleFor(x => x.Address)
             .NotEmpty().WithMessage("La dirección es obligatoria")
             .MinimumLength(10).WithMessage("La dirección debe tener al menos 10 caracteres")
             .MaximumLength(100).WithMessage("La dirección no puede superar los 100 caracteres");

         RuleFor(x => x.IdentityCardNumber)
             .NotEmpty().WithMessage("La cédula es obligatoria")
             .Length(11).WithMessage("La cédula debe tener exactamente 11 dígitos")
             .Matches(@"^\d{11}$").WithMessage("La cédula solo puede contener números");
         RuleFor(x => x.SexId)
             .GreaterThan(0).WithMessage("Debe seleccionar un sexo valido.");

         When(x => !string.IsNullOrWhiteSpace(x.StudentLicence), () =>
         {
             RuleFor(x => x.StudentLicence)
                 .MinimumLength(3).WithMessage("La matrícula debe tener al menos 3 caracteres")
                 .MaximumLength(9).WithMessage("La matrícula no puede superar los 9 caracteres")
                 .Matches(@"^[A-Z0-9\-]+$")
                 .WithMessage("La matrícula solo puede contener letras mayúsculas, números y guiones");
         });
     }
 }
 
 internal class CreateReaderEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost(EndpointSettings.ReadersEndpoint, async (
                    [FromBody] CreateReaderCommand command,
                    ISender sender,
                    IEndpointWrapper<CreateReaderEndpoint> wrapper,
                    CancellationToken cancellationToken = default
                ) =>
                {
                    return await wrapper.ExecuteAsync<IApiResult>(async () =>
                    {
                        return await sender.SendAndValidateAsync(command, cancellationToken);
                    });
                })
                .RequireAuthorization(AuthorizationPolicies.AllowAdminsOnly)
                .RequireCors(CorsPolicies.DefaultPolicy)
                .DisableAntiforgery()
                .Accepts<CreateReaderCommand>(false, ApplicationContentTypes.ApplicationJson)
                .Produces<ApiResult<ReaderResponse>>(200, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(409, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(403, ApplicationContentTypes.ApplicationJson)
                .WithTags(nameof(Reader))
                .WithName(nameof(CreateReaderEndpoint))
                .WithDescription("Crea un nuevo lector en el sistema");
        }
    }

    
    public class CreateReaderCommandHandler : ICommandHandler<CreateReaderCommand, IApiResult>
    {
        private readonly IBibliotecaUtecoDbContext _context;

        public CreateReaderCommandHandler(IBibliotecaUtecoDbContext context)
        {
            _context = context;
        }

        public async Task<IApiResult> HandleAsync(CreateReaderCommand request, CancellationToken cancellationToken = default)
        {
            if (await _context.Readers.AnyAsync(r => r.IdentityCardNumber == request.IdentityCardNumber, cancellationToken))
            {
                return ApiResult<ReaderResponse>.BuildFailure(
                    HttpStatus.Conflict,
                    "Ya existe un lector registrado con esa cédula");
            }

            if (!string.IsNullOrWhiteSpace(request.StudentLicence))
            {
                var normalizedLicence = request.StudentLicence.ToUpper().Trim();
                
                if (await _context.Readers.AnyAsync(r => r.StudentLicence == normalizedLicence, cancellationToken))
                {
                    return ApiResult<ReaderResponse>.BuildFailure(
                        HttpStatus.Conflict,
                        "Ya existe un lector registrado con esa matrícula");
                }
            }

            if (await _context.Readers.AnyAsync(r => r.PhoneNumber == request.PhoneNumber, cancellationToken))
            {
                return ApiResult<ReaderResponse>.BuildFailure(
                    HttpStatus.Conflict,
                    "Ya existe un lector registrado con ese número de teléfono");
            }

          

            var insertion = await _context.Readers.AddAsync(Reader.Create(request), cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            if (insertion.Entity.Id == 0)
            {
                return ApiResult<ReaderResponse>.BuildFailure(
                    HttpStatus.BadRequest,
                    "Error al crear el lector");
            }

            _context.ChangeTracker.Clear();

            var createdReader = await _context.Readers.GetByIdAsync(insertion.Entity.Id, cancellationToken);

            if (createdReader == null)
            {
                return ApiResult<ReaderResponse>.BuildFailure(
                    HttpStatus.BadRequest,
                    "El lector no pudo ser encontrado tras su creación");
            }

            return ApiResult<ReaderResponse>.BuildSuccess(createdReader.ToResponse());
        }
    }