namespace BibliotecaUteco.Features.LoansFeatures.Queries;
public class GetLoansByFilter : ICommand<IApiResult>
{
    [FromQuery(Name = "identityCardNumber" ), JsonPropertyName("identityCardNumber"), MaxLength(11), MinLength(11)]
    [Description("Cédula de identidad del lector (11 dígitos)")]
    public string? IdentityCardNumber { get; set; }

    [FromQuery(Name = "studentLicence" ), JsonPropertyName("studentLicence"), MaxLength(9), MinLength(3)]
    [Description("Matrícula estudiantil")]
    public string? StudentLicence { get; set; }

    [FromQuery(Name = "justPendingOnes" ), JsonPropertyName("justPendingOnes")]
    [Description("Solo préstamos pendientes/activos")]
    public bool? JustPendingOnes { get; set; }

    [FromQuery(Name = "justExceededOnes" ), JsonPropertyName("justExceededOnes")]
    [Description("Solo préstamos excedidos/vencidos")]
    public bool? JustExceededOnes { get; set; }

    [FromQuery(Name = "justReturnedOnes" ), JsonPropertyName("justReturnedOnes")]
    [Description("Solo préstamos entregados/devueltos")]
    public bool? JustReturnedOnes { get; set; }

    [FromQuery(Name = "skip" ), JsonPropertyName("skip"), Range(0, int.MaxValue)]
    [Description("Cantidad de registros a saltar")]
    public int? Skip { get; set; } = 0;

    [FromQuery(Name = "take" ), JsonPropertyName("take"), Range(1, 100)]
    [Description("Cantidad de registros a obtener (1-100)")]
    public int? Take { get; set; } = 5;
}

public class GetLoansByFilterValidator : AbstractValidator<GetLoansByFilter>
{
    public GetLoansByFilterValidator()
    {
        When(
            x => !string.IsNullOrWhiteSpace(x.IdentityCardNumber),
            () =>
            {
                RuleFor(x => x.IdentityCardNumber)
                    .Length(11)
                    .WithMessage("La cédula debe tener exactamente 11 dígitos")
                    .Matches(@"^\d{11}$")
                    .WithMessage("La cédula solo puede contener números");
            }
        );

        When(
            x => !string.IsNullOrWhiteSpace(x.StudentLicence),
            () =>
            {
                RuleFor(x => x.StudentLicence)
                    .MinimumLength(3)
                    .WithMessage("La matrícula debe tener al menos 3 caracteres")
                    .MaximumLength(9)
                    .WithMessage("La matrícula no puede superar los 9 caracteres")
                    .Matches(@"^[0-9\-]+$")
                    .WithMessage("La matrícula solo puede contener números y guiones");
            }
        );

        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Skip debe ser mayor o igual a 0");

        RuleFor(x => x.Take)
            .InclusiveBetween(1, 100)
            .WithMessage("Take debe estar entre 1 y 100");

        RuleFor(x => x)
            .Must(x =>
            {
                var statusFiltersCount = new[] { x.JustPendingOnes, x.JustExceededOnes, x.JustReturnedOnes }
                    .Count(f => f.HasValue && f.Value);
                return statusFiltersCount <= 1;
            })
            .WithMessage("Solo puede seleccionar un tipo de estado a la vez (pendientes, excedidos o entregados)");
    }
}

internal class GetLoansByFilterEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                EndpointSettings.LoansEndpoint + "/by-filter",
                async (
                    [AsParameters] GetLoansByFilter command,
                    ISender sender,
                    IEndpointWrapper<GetLoansByFilterEndpoint> wrapper,
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
            .Produces<SuccessApiResult<List<LoanResponse>>>(200, ApplicationContentTypes.ApplicationJson)
            .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)

            .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)

            .Produces<InternalServerErrorApiResult>(404, ApplicationContentTypes.ApplicationJson)
                            .Produces<ForbiddenApiResult>(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Loan))
            .WithName(nameof(GetLoansByFilterEndpoint))
            .WithDescription("Obtiene préstamos filtrados por cédula, matrícula o estado");
    }
}

public class GetLoansByFilterHandler(IBibliotecaUtecoDbContext context) : ICommandHandler<GetLoansByFilter, IApiResult>
{
    public async Task<IApiResult> HandleAsync(
        GetLoansByFilter request,
        CancellationToken cancellationToken = default
    )
    {
       
        var response = await context.Loans
            .GetByFilter(
                request.IdentityCardNumber, 
                request.StudentLicence,
                request.JustPendingOnes,
                request.JustReturnedOnes, 
                request.JustExceededOnes, 
                request.Skip ?? 0, 
                request.Take ?? 5,
                cancellationToken
            );

        return new SuccessApiResult<List<LoanResponse>>(response.Select(l => l.ToResponse()).ToList());
    }
}