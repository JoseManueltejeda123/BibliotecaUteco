namespace BibliotecaUteco.Features.Reports.Queries;

public class GetLoansPerMonthCommand : ICommand<IApiResult>
{
    [JsonPropertyName("year"), FromQuery(Name = "year"), Required]
    [Description("El año del reporte")]
    [Range(1900, 2100)]
    public required int  Year { get; set; }
}

// Validator
public class GetLoansPerMonthCommandValidator : AbstractValidator<GetLoansPerMonthCommand>
{
    public GetLoansPerMonthCommandValidator()
    {
        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .WithMessage("El año debe estar entre 1900 y 2100");
    }
}

// Endpoint
internal class GetLoansPerMonthEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                EndpointSettings.ReportsEndpoint + "/loans-per-month",
                async (
                    [AsParameters] GetLoansPerMonthCommand command,
                    ISender sender,
                    IEndpointWrapper<GetLoansPerMonthEndpoint> wrapper,
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
            .Produces<SuccessApiResult<LoansPerMonthResponse>>(
                200,
                ApplicationContentTypes.ApplicationJson
            )
            .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)
            .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .Produces<InternalServerErrorApiResult>(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Loan))
            .WithName(nameof(GetLoansPerMonthEndpoint))
            .WithDescription("Obtiene la cantidad de préstamos por mes en un año específico");
    }
}

// Handler
public class GetLoansPerMonthCommandHandler
    : ICommandHandler<GetLoansPerMonthCommand, IApiResult>
{
    private readonly IBibliotecaUtecoDbContext _context;

    // Diccionario de meses
    private readonly Dictionary<int, string> _meses = new Dictionary<int, string>
    {
        { 1, "Enero" },
        { 2, "Febrero" },
        { 3, "Marzo" },
        { 4, "Abril" },
        { 5, "Mayo" },
        { 6, "Junio" },
        { 7, "Julio" },
        { 8, "Agosto" },
        { 9, "Septiembre" },
        { 10, "Octubre" },
        { 11, "Noviembre" },
        { 12, "Diciembre" }
    };

    public GetLoansPerMonthCommandHandler(IBibliotecaUtecoDbContext context)
    {
        _context = context;
    }

    public async Task<IApiResult> HandleAsync(
        GetLoansPerMonthCommand request,
        CancellationToken cancellationToken = default
    )
    {
        // Query para obtener préstamos agrupados por mes
        var loansGrouped = await _context.Loans
            .Where(loan => loan.CreatedAt.Year == request.Year)
            .GroupBy(loan => loan.CreatedAt.Month)
            .Select(group => new
            {
                Month = group.Key,
                Count = group.Count()
            })
            .ToListAsync(cancellationToken);
        
        var monthLoanCount = new Dictionary<string, int>();
        
        foreach (var mes in _meses)
        {
            var loanData = loansGrouped.FirstOrDefault(x => x.Month == mes.Key);
            monthLoanCount[mes.Value] = loanData?.Count ?? 0;
        }

        var response = new LoansPerMonthResponse
        {
            MonthLoanCount = monthLoanCount
        };

        return new SuccessApiResult<LoansPerMonthResponse>(response);
    }
}