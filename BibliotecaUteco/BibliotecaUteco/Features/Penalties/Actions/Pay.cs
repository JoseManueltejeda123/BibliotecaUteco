using BibliotecaUteco.Utilities;

namespace BibliotecaUteco.Features.Penalties.Actions;

public class PayPenaltyCommand : CommandWithUserCredentials, ICommand<IApiResult>
{
    [FromBody, JsonPropertyName("penaltyId"), Required, Range(1, int.MaxValue)]
    [Description("ID de la penalización")]
    public int PenaltyId { get; set; }

    [FromBody, JsonPropertyName("givenAmount"), Required]
    [Description("Monto entregado por el cliente")]
    public double GivenAmount { get; set; }
}

// Validator
public class PayPenaltyCommandValidator : AbstractValidator<PayPenaltyCommand>
{
    public PayPenaltyCommandValidator()
    {
        RuleFor(x => x.PenaltyId)
            .GreaterThan(0)
            .WithMessage("Debe proporcionar un ID de penalización válido");

        RuleFor(x => x.GivenAmount)
            .GreaterThan(0)
            .WithMessage("El monto entregado debe ser mayor a 0")
            .Must((command, givenAmount) => givenAmount >= 0)
            .WithMessage("El monto entregado no puede ser negativo");
    }
}

// Endpoint
internal class PayPenaltyEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
                EndpointSettings.PenaltiesEndpoint + "/pay",
                async (
                    [FromBody] PayPenaltyCommand command,
                    ISender sender,
                    HttpContext context,
                    IEndpointWrapper<PayPenaltyEndpoint> wrapper,
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
            .Accepts<PayPenaltyCommand>(false, ApplicationContentTypes.ApplicationJson)
            .Produces<SuccessApiResult<PenaltyResponse>>(
                200,
                ApplicationContentTypes.ApplicationJson
            )
            .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)
            .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(409, ApplicationContentTypes.ApplicationJson)
            .Produces<InternalServerErrorApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .Produces<ForbiddenApiResult>(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Penalty))
            .WithName(nameof(PayPenaltyEndpoint))
            .WithDescription("Registra el pago de una penalización");
    }
}

// Handler
public class PayPenaltyCommandHandler(IBibliotecaUtecoDbContext context)
    : ICommandHandler<PayPenaltyCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(
        PayPenaltyCommand request,
        CancellationToken cancellationToken = default
    )
    {
        if (
            await context
                .Penalties.IgnoreAutoIncludes()
                .AsSplitQuery()
                .FirstOrDefaultAsync(p => p.Id == request.PenaltyId, cancellationToken)
                is var penalty
            && penalty is null
        )
        {
            return new NotFoundApiResult(
                $"No se encontró la penalización con ID {request.PenaltyId}"
            );
        }

        if (!penalty.IsDue)
        {
            return new ConflictApiResult("Esta penalización ya ha sido pagada");
        }

        if (request.GivenAmount < penalty.TotalAmount)
        {
            return new BadRequestApiResult(
                $"El monto entregado (RD$ {request.GivenAmount:N2}) es insuficiente. El total a pagar es RD$ {penalty.TotalAmount:N2}"
            );
        }

        await using var transaction = await context.Database.BeginTransactionAsync(
            cancellationToken
        );

        try
        {
            var transactionInsertion = await context.Transactions.AddAsync(
                Transaction.Create(request.CurrentUserId, penalty.TotalAmount),
                cancellationToken
            );
            await context.SaveChangesAsync(cancellationToken);

            if (transactionInsertion.Entity.Id == 0)
            {
                await transaction.RollbackAsync(cancellationToken);
                return new BadRequestApiResult("No se pudo crear la transacción de pago");
            }

            if (!penalty.Pay(request.GivenAmount, transactionInsertion.Entity.Id))
            {
                await transaction.RollbackAsync(cancellationToken);
                return new BadRequestApiResult("No pudimos marcar la penalización como paga");
            }

            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            context.ChangeTracker.Clear();

            if (await context.Penalties.GetByIdAsync(penalty.Id) is var result && result is null)
            {
                return new BadRequestApiResult("No se pudo recuperar la penalización actualizada");
            }

            return new SuccessApiResult<PenaltyResponse>(result.ToResponse());
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
