namespace BibliotecaUteco.Features.Penalties.Queries;

public class GetPenaltiesByFilterCommand : ICommand<IApiResult>
{
    [FromQuery(Name = "isDue"), JsonPropertyName("isDue")]
    [Description("Filtrar por penalizaciones pendientes (true) o pagadas (false)")]
    public bool? IsDue { get; set; }

    [FromQuery(Name = "loanId"), JsonPropertyName("loanId")]
    [Description("Filtrar por ID de préstamo")]
    public int LoanId { get; set; } = 0;

    [
        FromQuery(Name = "take"),
        JsonPropertyName("take"),
        Range(1, 10),
        Description("Cantidad de penalizaciones a buscar")
    ]
    public int Take { get; set; } = 10;

    [
        FromQuery(Name = "skip"),
        JsonPropertyName("skip"),
        Range(0, int.MaxValue),
        Description("Cantidad de penalizaciones a omitir")
    ]
    public int Skip { get; set; } = 0;
}

// Validator
public class GetPenaltiesByFilterValidator : AbstractValidator<GetPenaltiesByFilterCommand>
{
    public GetPenaltiesByFilterValidator()
    {
        RuleFor(x => x.Take)
            .InclusiveBetween(1, 10)
            .WithMessage("El numero de penalizaciones debe de ser mayor a 1 y menor a 10");

        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El numero de panelizaciones a omitir debe de ser mayor o igual a 0");

        RuleFor(x => x.LoanId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El ID del préstamo debe ser mayor o igual a 0");
    }
}

// Endpoint
internal class GetPenaltiesByFilterEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                EndpointSettings.PenaltiesEndpoint + "/by-filter",
                async (
                    [AsParameters] GetPenaltiesByFilterCommand byFilterCommand,
                    ISender sender,
                    IEndpointWrapper<GetPenaltiesByFilterEndpoint> wrapper,
                    CancellationToken cancellationToken = default
                ) =>
                {
                    return await wrapper.ExecuteAsync<IApiResult>(async () =>
                    {
                        return await sender.SendAndValidateAsync(
                            byFilterCommand,
                            cancellationToken
                        );
                    });
                }
            )
            .RequireAuthorization(AuthorizationPolicies.AllowAuthorizedUsers)
            .RequireCors(CorsPolicies.DefaultPolicy)
            .Produces<SuccessApiResult<List<PenaltyResponse>>>(
                200,
                ApplicationContentTypes.ApplicationJson
            )
            .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)
            .Produces<InternalServerErrorApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .Produces<ForbiddenApiResult>(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Penalty))
            .WithName(nameof(GetPenaltiesByFilterEndpoint))
            .WithDescription("Obtiene una lista de penalizaciones con filtros opcionales");
    }
}

// Handler
public class GetPenaltiesByFilterCommandHandler(IBibliotecaUtecoDbContext context)
    : ICommandHandler<GetPenaltiesByFilterCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(
        GetPenaltiesByFilterCommand request,
        CancellationToken cancellationToken = default
    )
    {
        var result = await context.Penalties.GetByFilterAsync(
            request.IsDue,
            request.LoanId,
            request.Skip,
            request.Take,
            cancellationToken
        );

        return new SuccessApiResult<List<PenaltyResponse>>(
            result.Select(p => p.ToResponse()).ToList()
        );
    }
}
