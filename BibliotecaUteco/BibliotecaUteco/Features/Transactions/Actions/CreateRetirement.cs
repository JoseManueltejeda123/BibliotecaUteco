using BibliotecaUteco.Utilities;

namespace BibliotecaUteco.Features.Transactions.Actions;

public class CreateRetirementTransactionsCommand : CommandWithUserCredentials, ICommand<IApiResult>
{
    [
        JsonPropertyName("amount"),
        FromBody,
        Description("La cantidad a retirar"),
        MinLength(0),
        Required
    ]
    public double Amount { get; set; }

    [
        JsonPropertyName("password"),
        FromBody,
        Description("La contraseña del usuario actual"),
        Required,
        MinLength(8),
        MaxLength(30)
    ]
    public string Password { get; set; } = "";
}

public class CreateRetirementTransactionCommandValidator
    : AbstractValidator<CreateRetirementTransactionsCommand>
{
    public CreateRetirementTransactionCommandValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("La cantidad a retirar debe de ser menor a 0");

        RuleFor(x => x.Password)
            .MinimumLength(8)
            .WithMessage("La contraseña debe de ser mayor a 8 caracteres")
            .MaximumLength(30)
            .WithMessage("La contraseña debe de ser menor a 30 caracteres");
    }
}

public class CreateRetirmentTransactionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                EndpointSettings.TransactionsEndpoint + "/retirement",
                async (
                    [FromBody] CreateRetirementTransactionsCommand command,
                    HttpContext context,
                    ISender sender,
                    IEndpointWrapper<CreateRetirmentTransactionEndpoint> endpointWrapper,
                    CancellationToken cancellationToken = default
                ) =>
                {
                    return await endpointWrapper.ExecuteAsync<IApiResult>(async () =>
                    {
                        command.SetCurrentUserId(
                            UserIdentityUtility.GetUserIdFromClaims(context.User)
                        );
                        return await sender.SendAndValidateAsync(command, cancellationToken);
                    });
                }
            )
            .RequireAuthorization(AuthorizationPolicies.AllowAdminsOnly)
            .RequireCors()
            .DisableAntiforgery()
            .Accepts<CreateRetirementTransactionsCommand>(
                false,
                ApplicationContentTypes.ApplicationJson
            )
            .Produces<SuccessApiResult<TransactionResponse>>(
                200,
                ApplicationContentTypes.ApplicationJson
            )
            .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
            .Produces<InternalServerErrorApiResult>(500, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(401, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Transaction))
            .WithName(nameof(CreateRetirmentTransactionEndpoint))
            .WithDescription("Crea una transaccion de retiro en la app");
    }
}

internal class CreateRetirementTransactinCommandHandler(IBibliotecaUtecoDbContext context)
    : ICommandHandler<CreateRetirementTransactionsCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(
        CreateRetirementTransactionsCommand request,
        CancellationToken cancellationToken = default
    )
    {
        if (
            await context.Transactions.SumAsync(x => x.Amount, cancellationToken: cancellationToken)
            < request.Amount
        )
        {
            return new BadRequestApiResult("El monto es muy alto para el estado actual de caja");
        }

        var hashedPassword = request.Password.Hash();
        if (
            !await context.Users.AnyAsync(
                u => u.Password == hashedPassword && u.Id == request.CurrentUserId,
                cancellationToken
            )
        )
        {
            return new NotFoundApiResult("Credenciales incorrectas");
        }

        /*if(await context.Transactions.AsNoTracking().IgnoreAutoIncludes().Where(t =>  t.Amount < 0).OrderByDescending(t => t.CreatedAt).FirstOrDefaultAsync(cancellationToken) is var lastTransaction && lastTransaction is not null)
        {
            if((DateTime.UtcNow - lastTransaction.CreatedAt).TotalDays < 15)
            {
                return new BadRequestApiResult(
                    $"No se pueden realizar de retiro en este momento ya que se hizo uno hace menos de 15 dias");
            }
        }*/

        var insertion = await context.Transactions.AddAsync(
            Transaction.Create(request.CurrentUserId, request.Amount * -1),
            cancellationToken
        );
        await context.SaveChangesAsync(cancellationToken);
        context.ChangeTracker.Clear();
        if (insertion.Entity.Id == 0)
        {
            return new BadRequestApiResult("No pudimos crear la transacción");
        }

        if (
            await context.Transactions.FirstOrDefaultAsync(
                t => t.Id == insertion.Entity.Id,
                cancellationToken
            )
                is var result
            && result is null
        )
        {
            return new BadRequestApiResult("No pudimos encontrar la transaccion creada");
        }

        return new SuccessApiResult<TransactionResponse>(result.ToResponse());
    }
}
