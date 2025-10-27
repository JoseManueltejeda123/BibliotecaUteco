using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Features.Transactions.Queries
{
    public class GetCashBoxSummaryCommand : CommandWithUserCredentials, ICommand<IApiResult>
    {
        
    }

    public class GetCashBoxSummaryEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                EndpointSettings.TransactionsEndpoint + "/summary",
                async (
                    [AsParameters] GetCashBoxSummaryCommand command,
                    IEndpointWrapper<GetCashBoxSummaryEndpoint> endpointWrapper,
                    ISender sender,
                    CancellationToken token = default
                ) =>
                {
                    return await endpointWrapper.ExecuteAsync<IApiResult>(async () =>
                    {
                        return await sender.SendAndValidateAsync(command, token);
                    });
                }
            )
            .RequireAuthorization(AuthorizationPolicies.AllowAdminsOnly)
            .RequireCors()
            .DisableAntiforgery()
            .Produces<SuccessApiResult<List<TransactionResponse>>>(
                200,
                ApplicationContentTypes.ApplicationJson
            )
            .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)
            .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .Produces<InternalServerErrorApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Transaction))
            .WithName(nameof(GetCashBoxSummaryEndpoint))
            .WithDescription("Retorna el resumen de la caja");
    }
}

internal class GetCashBoxSummaryCommandHandler(IBibliotecaUtecoDbContext context)
    : ICommandHandler<GetCashBoxSummaryCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(
        GetCashBoxSummaryCommand request,
        CancellationToken cancellationToken = default
    )
    {
        var summary = await context.Transactions
            .AsNoTracking()
            .GroupBy(t => 1) // Agrupa todo en un solo grupo
            .Select(g => new CashBoxSummaryResponse
            {
                // Estado de caja: suma de todos los montos (positivos y negativos)
                CashBoxState = g.Sum(t => t.Amount),
                
                LastDepositAmount = g
                    .Where(t => t.Amount > 0)
                    .OrderByDescending(t => t.CreatedAt)
                    .Select(t => t.Amount)
                    .FirstOrDefault(),
                
                LastDepositDate = g
                    .Where(t => t.Amount > 0)
                    .OrderByDescending(t => t.CreatedAt)
                    .Select(t => (DateTime?)t.CreatedAt)
                    .FirstOrDefault(),
                
                LastRetirementAmount = g
                    .Where(t => t.Amount < 0)
                    .OrderByDescending(t => t.CreatedAt)
                    .Select(t => Math.Abs(t.Amount))
                    .FirstOrDefault(),
                
                LastRetirementDate = g
                    .Where(t => t.Amount < 0)
                    .OrderByDescending(t => t.CreatedAt)
                    .Select(t => (DateTime?)t.CreatedAt)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (summary == null)
        {
            return new NotFoundApiResult(
                "No se encontraron transacciones en el sistema"
            );
        }

        return new SuccessApiResult<CashBoxSummaryResponse>(
            summary,
            "Resumen de caja obtenido correctamente"
        );
    }
}

}