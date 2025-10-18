namespace BibliotecaUteco.Features.ReadersFeatures.Queries;

public class GetReadersByFilterCommand : ICommand<IApiResult>
{
    [FromQuery(Name = "identityCardNumber"), JsonPropertyName("identityCardNumber")]
    [Description("Cédula del lector a buscar")]
    [MaxLength(11)]
    public string? IdentityCardNumber { get; set; }

    [FromQuery(Name = "studentLicence"), JsonPropertyName("studentLicence")]
    [Description("Matrícula del lector a buscar")]
    [MaxLength(9)]
    public string? StudentLicence { get; set; }

    [FromQuery(Name = "skip"), JsonPropertyName("skip"), Range(0, int.MaxValue)]
    [Description("Cantidad de lectores a omitir")]
    public int Skip { get; set; } = 0;

    [FromQuery(Name = "take"), JsonPropertyName("take"), Range(1, 15)]
    [Description("Cantidad de lectores a tomar. 15 por defecto")]
    public int Take { get; set; } = 15;
}

public class GetReaderByFilterCommandValidator : AbstractValidator<GetReadersByFilterCommand>
{
    public GetReaderByFilterCommandValidator()
    {
        
        When(x => !string.IsNullOrWhiteSpace(x.IdentityCardNumber), () =>
        {
            RuleFor(x => x.IdentityCardNumber)
                .MinimumLength(1).WithMessage("La cédula debe tener mas de 1 dígito")                
                .MaximumLength(11).WithMessage("La cédula no debe tener mas de 11 dígitos");

        });

        When(x => !string.IsNullOrWhiteSpace(x.StudentLicence), () =>
        {
            RuleFor(x => x.StudentLicence)
                .MinimumLength(1).WithMessage("La matrícula debe tener al menos 1 caracteres")
                .MaximumLength(9).WithMessage("La matrícula no puede superar los 9 caracteres");
        });
        
        RuleFor(x => x.Skip).GreaterThanOrEqualTo(0).WithMessage("La cantidad de lectores a omitir debe de ser mayor o igual a 0.");
        RuleFor(x => x.Take).InclusiveBetween(1, 15).WithMessage("La cantidad de lectores a tomar debe de ser mayor a 0 y menor a 15");

    }
}
internal class GetReaderByFilterEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(EndpointSettings.ReadersEndpoint + "/by-filter", async (
                [AsParameters] GetReadersByFilterCommand command,
                ISender sender,
                IEndpointWrapper<GetReaderByFilterEndpoint> wrapper,
                CancellationToken cancellationToken = default
            ) =>
            {
                return await wrapper.ExecuteAsync<IApiResult>(async () =>
                {
                    return await sender.SendAndValidateAsync(command, cancellationToken);
                });
            })
            .RequireAuthorization(AuthorizationPolicies.AllowAuthorizedUsers)
            .RequireCors(CorsPolicies.DefaultPolicy)
            .DisableAntiforgery()
            .Produces<ApiResult<List<ReaderResponse>>>(200, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Reader))
            .WithName(nameof(GetReaderByFilterEndpoint))
            .WithDescription("Busca lectores por cédula o matrícula");
    }
}

public class GetReaderByFilterCommandHandler : ICommandHandler<GetReadersByFilterCommand, IApiResult>
{
    private readonly IBibliotecaUtecoDbContext _context;

    public GetReaderByFilterCommandHandler(IBibliotecaUtecoDbContext context)
    {
        _context = context;
    }

    public async Task<IApiResult> HandleAsync(GetReadersByFilterCommand request, CancellationToken cancellationToken = default)
    {
        var response = await _context.Readers.GetByFilterAsync(request.IdentityCardNumber, request.StudentLicence,
            request.Skip, request.Take, cancellationToken);
        return ApiResult<List<ReaderResponse>>.BuildSuccess(response.Select(reader => reader.ToResponse()).ToList());
    }
}