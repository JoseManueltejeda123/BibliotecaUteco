namespace BibliotecaUteco.Features.Reports.Queries;

public class GetTopBooksByDateCommand : ICommand<IApiResult>
{
    [FromQuery(Name = "year"), JsonPropertyName("year")]
    [Description("El año del reporte")]
    [Range(1900, 2100)]
    public required int Year { get; set; }

    [FromQuery(Name = "month"), JsonPropertyName("month")]
    [Description("El mes del reporte")]
    [Range(1, 12)]
    public required int Month { get; set; }
}

// Validator
public class GetTopBooksByDateCommandValidator : AbstractValidator<GetTopBooksByDateCommand>
{
    public GetTopBooksByDateCommandValidator()
    {
        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.Now.Year)
            .WithMessage("El año debe estar entre 1900 y 2100");

        RuleFor(x => x.Month)
            .InclusiveBetween(1, 12)
            .WithMessage("El mes debe estar entre 1 y 12");
    }
}

// Endpoint
internal class GetTopBooksByDateEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                EndpointSettings.ReportsEndpoint + "/top-books-by-date",
                async (
                    [AsParameters] GetTopBooksByDateCommand command,
                    ISender sender,
                    IEndpointWrapper<GetTopBooksByDateEndpoint> wrapper,
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
            .Produces<SuccessApiResult<TopBooksResponse>>(
                200,
                ApplicationContentTypes.ApplicationJson
            )
            .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)
            .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .Produces<InternalServerErrorApiResult>(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Book))
            .WithName(nameof(GetTopBooksByDateEndpoint))
            .WithDescription("Obtiene los libros más prestados por mes y año");
    }
}

// Handler
public class GetTopBooksByDateCommandHandler(IBibliotecaUtecoDbContext context)
    : ICommandHandler<GetTopBooksByDateCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(
        GetTopBooksByDateCommand request,
        CancellationToken cancellationToken = default
    )
    {
        var start = new DateTime(request.Year, request.Month, 1);
        var end = start.AddMonths(1);

        var topBooks = await context.Books
            .AsNoTracking()
            .AsSplitQuery()
            .Where(b => b.Loans
                .Count(l => l.CreatedAt >= start && l.CreatedAt < end) >= 1)
            .OrderByDescending(b => b.Loans
                .Count(l => l.CreatedAt >= start && l.CreatedAt < end))
            .Take(10)
            .Select(b => new Book
            {
                Id = b.Id,
                Name = b.Name,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt,
                Synopsis = b.Synopsis,
                Authors = b.Authors,
                Genres = b.Genres,
                Stock = b.Stock,
                CoverUrl = b.CoverUrl,
                AvailableAmount = b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null),
                ActiveLoansCount = b.Loans.Count(l => l.Loan.ReturnedDate == null),
                LoansCount = b.Loans.Count(l => l.CreatedAt >= start && l.CreatedAt < end),
                IsDisabled = b.IsDisabled
            })
            .ToListAsync(cancellationToken);

        var response = new TopBooksResponse
        {
            Month = request.Month,
            Year = request.Year,
            Books = topBooks.Select(book => book.ToResponse()).ToList()
        };

        return new SuccessApiResult<TopBooksResponse>(response);
    }
}