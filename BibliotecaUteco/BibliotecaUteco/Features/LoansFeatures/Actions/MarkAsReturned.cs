using BibliotecaUteco.Utilities;

namespace BibliotecaUteco.Features.LoansFeatures.Actions;


public class MarkLoanAsReturnedCommand: CommandWithUserCredentials, ICommand<IApiResult>
{
    [FromBody, JsonPropertyName("loanId"), Required, Range(1, int.MaxValue)]
    [Description("ID del préstamo")]
    public int LoanId { get; set; }
}

// Validator
public class MarkLoanAsReturnedCommandValidator : AbstractValidator<MarkLoanAsReturnedCommand>
{
    public MarkLoanAsReturnedCommandValidator()
    {
        RuleFor(x => x.LoanId)
            .GreaterThan(0)
            .WithMessage("Debe proporcionar un ID de préstamo válido");
    }
}

// Endpoint
internal class MarkLoanAsReturnedEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
                EndpointSettings.LoansEndpoint + "/mark-as-returned",
                async (
                    [FromBody] MarkLoanAsReturnedCommand command,
                    ISender sender,
                    HttpContext context,
                    IEndpointWrapper<MarkLoanAsReturnedEndpoint> wrapper,
                    CancellationToken cancellationToken = default
                ) =>
                {
                    command.SetCurrentUserId(UserIdentityUtility.GetUserIdFromClaims(context.User));
                    return await wrapper.ExecuteAsync<IApiResult>(async () =>
                    {
                        return await sender.SendAndValidateAsync(command, cancellationToken);
                    });
                }
            )
            .RequireAuthorization(AuthorizationPolicies.AllowAuthorizedUsers)
            .RequireCors(CorsPolicies.DefaultPolicy)
            .DisableAntiforgery()
            .Accepts<MarkLoanAsReturnedCommand>(false, ApplicationContentTypes.ApplicationJson)
            .Produces<ApiResult<LoanResponse>>(200, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(409, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(403, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Loan))
            .WithName(nameof(MarkLoanAsReturnedEndpoint))
            .WithDescription("Marca un préstamo como entregado/devuelto");
    }
}

// Handler
public class MarkLoanAsReturnedCommandHandler(IBibliotecaUtecoDbContext context)
    : ICommandHandler<MarkLoanAsReturnedCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(
        MarkLoanAsReturnedCommand request,
        CancellationToken cancellationToken = default
    )
    {
        var loan = await context.Loans
            .IgnoreAutoIncludes()
            .AsSplitQuery()
            .FirstOrDefaultAsync(l => l.Id == request.LoanId, cancellationToken);

        if (loan is null)
        {
            return ApiResult<LoanResponse>.BuildFailure(
                HttpStatus.NotFound,
                $"No se encontró el préstamo con ID {request.LoanId}"
            );
        }

        if (loan.ReturnedDate is not null)
        {
            return ApiResult<LoanResponse>.BuildFailure(
                HttpStatus.Conflict,
                "El préstamo ya ha sido marcado como devuelto"
            );
        }

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            loan.ReturnedDate = DateTime.UtcNow;
            await context.SaveChangesAsync(cancellationToken);

            // Verificar si se debe crear una penalización
            if (loan.ReturnedDate.Value > loan.DueDate)
            {
                var penaltyInsertion = await context.Penalties.AddAsync(
                    Penalty.Create(loan),
                    cancellationToken
                );
                await context.SaveChangesAsync(cancellationToken);

                if (penaltyInsertion.Entity.Id == 0)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return ApiResult<LoanResponse>.BuildFailure(
                        HttpStatus.BadRequest,
                        "No pudimos crear la penalización. El préstamo no se marcó como entregado"
                    );
                }
            }

            await transaction.CommitAsync(cancellationToken);

            context.ChangeTracker.Clear();

            var updatedLoan = await context.Loans.GetByIdAsync(loan.Id, cancellationToken);

            if (updatedLoan is null)
            {
                
                return ApiResult<LoanResponse>.BuildFailure(
                    HttpStatus.BadRequest,
                    "No se pudo recuperar el préstamo actualizado"
                );
            }

            return ApiResult<LoanResponse>.BuildSuccess(updatedLoan.ToResponse());
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}