namespace BibliotecaUteco.Features.LoansFeatures.Actions;

public class CreateLoanCommand : ICommand<IApiResult>
{
    [FromBody, JsonPropertyName("readerId"), Required, Range(1, int.MaxValue)]
    [Description("ID del lector")]
    public int ReaderId { get; set; }

    [FromBody, JsonPropertyName("bookIds"), Required, MinLength(1), MaxLength(10)]
    [Description("Lista de IDs de libros (1-10 libros)")]
    public List<int> BookIds { get; set; } = new();

    [FromBody, JsonPropertyName("maxLoanDays"), Required, Range(1, 30)]
    [Description("Días máximos del préstamo (7-14 días)")]
    public int MaxLoanDays { get; set; } = 14;
}

// Validator
public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
{
    public CreateLoanCommandValidator()
    {
        RuleFor(x => x.ReaderId).GreaterThan(0).WithMessage("Debe seleccionar un lector válido");

        RuleFor(x => x.BookIds)
            .NotEmpty()
            .WithMessage("Debe incluir al menos un libro")
            .Must(x => x.Count >= 1 && x.Count <= 10)
            .WithMessage("Debe prestar entre 1 y 10 libros")
            .Must(x => x.All(id => id > 0))
            .WithMessage("Todos los IDs de libros deben ser válidos")
            .Must(x => x.Distinct().Count() == x.Count)
            .WithMessage("No puede incluir libros duplicados");

        RuleFor(x => x.MaxLoanDays)
            .InclusiveBetween(1, 30)
            .WithMessage("Los días de préstamo deben estar entre 1 y 30 dias");
    }
}

// Endpoint
internal class CreateLoanEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                EndpointSettings.LoansEndpoint,
                async (
                    [FromBody] CreateLoanCommand command,
                    ISender sender,
                    IEndpointWrapper<CreateLoanEndpoint> wrapper,
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
            .Accepts<CreateLoanCommand>(false, ApplicationContentTypes.ApplicationJson)
            .Produces<SuccessApiResult<LoanResponse>>(200, ApplicationContentTypes.ApplicationJson)
            .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)
            .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(409, ApplicationContentTypes.ApplicationJson)
            .Produces<InternalServerErrorApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .Produces<ForbiddenApiResult>(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Loan))
            .WithName(nameof(CreateLoanEndpoint))
            .WithDescription("Crea un nuevo préstamo de libros");
    }
}

// Handler
public class CreateLoanCommandHandler(IBibliotecaUtecoDbContext context)
    : ICommandHandler<CreateLoanCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(
        CreateLoanCommand request,
        CancellationToken cancellationToken = default
    )
    {
        if (
            await context
                .Readers.AsNoTracking()
                .IgnoreAutoIncludes()
                .FirstOrDefaultAsync(r => r.Id == request.ReaderId, cancellationToken)
                is var reader
            && reader is null
        )
        {
            return new NotFoundApiResult($"No se encontró el lector con ID {request.ReaderId}");
        }

        if (
            await context.Loans.AnyAsync(
                l => l.ReaderId == request.ReaderId && l.ReturnedDate == null,
                cancellationToken
            )
        )
        {
            return new ConflictApiResult(
                "El lector tiene préstamos sin devolver y no puede realizar nuevos préstamos"
            );
        }

        var books = await context
            .Books.AsNoTracking()
            .AsSingleQuery()
            .IgnoreAutoIncludes()
            .Where(b => request.BookIds.Contains(b.Id))
            .Select(b => new Book()
            {
                Id = b.Id,
                Name = b.Name,
                AvailableAmount = b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null),
            })
            .ToListAsync(cancellationToken);

        if (books.Count != request.BookIds.Count)
        {
            var foundIds = books.Select(b => b.Id).ToList();
            var missingIds = request.BookIds.Except(foundIds).ToList();

            var missingBooks = books.Where(b => missingIds.Contains(b.Id)).ToList();

            return new NotFoundApiResult(
                $"No se encontraron los siguientes libros: {string.Join(", ", missingIds, missingBooks)}"
            );
        }

        var unavailableBooks = books.Where(b => (b.AvailableAmount <= 0)).ToList();
        if (unavailableBooks.Any())
        {
            return new ConflictApiResult(
                $"Los siguientes libros no están disponibles: {string.Join(", ", unavailableBooks.Select(b => b.Name))}"
            );
        }

        var insertion = await context.Loans.AddAsync(Loan.Create(request), cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        if (insertion.Entity.Id == 0)
        {
            return new BadRequestApiResult("Error al crear el préstamo");
        }

        context.ChangeTracker.Clear();

        var createdLoan = await context.Loans.GetByIdAsync(insertion.Entity.Id, cancellationToken);

        if (createdLoan == null)
        {
            return new BadRequestApiResult("El préstamo no pudo ser encontrado tras su creación");
        }

        return new SuccessApiResult<LoanResponse>(createdLoan.ToResponse());
    }
}
