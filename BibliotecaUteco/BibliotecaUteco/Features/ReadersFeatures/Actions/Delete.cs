using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Features.ReadersFeatures.Actions
{
    public class DeleteReaderCommand : ICommand<IApiResult>
    {
        [FromQuery(Name = "readerId"), JsonPropertyName("readerId"), Range(1, int.MaxValue)]
        [Description("ID único del lector a eliminar.")]
        public int ReaderId { get; set; }
    }

    public class DeleteReaderCommandValidator : AbstractValidator<DeleteReaderCommand>
    {
        public DeleteReaderCommandValidator()
        {
            RuleFor(x => x.ReaderId)
                .GreaterThan(0)
                .WithMessage("Debe especificar un ID de lector válido para eliminar.");
        }
    }

    internal class DeleteReaderEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete(
                    EndpointSettings.ReadersEndpoint + "/delete",
                    async (
                        [AsParameters] DeleteReaderCommand command,
                        ISender sender,
                        IEndpointWrapper<DeleteReaderEndpoint> wrapper,
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
            .Produces<ApiResult<bool>>(200, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(403, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Reader))
            .WithName(nameof(DeleteReaderEndpoint))
            .WithDescription("Elimina un lector existente del sistema.");
        }
    }

    
public class DeleteReaderCommandHandler(IBibliotecaUtecoDbContext context) : ICommandHandler<DeleteReaderCommand, IApiResult>
{
    
    public async Task<IApiResult> HandleAsync(DeleteReaderCommand request, CancellationToken cancellationToken = default)
    {


        if (await context.Readers.AsNoTracking().FirstOrDefaultAsync(r => r.Id == request.ReaderId, cancellationToken) is var reader && reader is null)
        {
            return ApiResult<bool>.BuildFailure(
                HttpStatus.NotFound,
                "No se encontró el lector especificado."
            );
        }

        if (await context.Loans.AnyAsync(r => r.ReaderId == request.ReaderId, cancellationToken))
        {
            return ApiResult<bool>.BuildFailure(
                HttpStatus.BadRequest,
                "Este usuario tiene prestamos activos."
            );
        }

        if (await context.Loans.AnyAsync(r => r.ReaderId == request.ReaderId && r.Penalty != null && r.Penalty.IsDue, cancellationToken))
        {
            return ApiResult<bool>.BuildFailure(
                HttpStatus.PaymentRequired,
                "Este usuario tiene una penalizacion sin pagar"
            );
        }

        var rows = await context.Readers.Where(r => r.Id == request.ReaderId).ExecuteDeleteAsync();
            await context.SaveChangesAsync(cancellationToken);

            if (rows <= 0) return ApiResult<bool>.BuildFailure(HttpStatus.BadRequest, "No se eliminó ningun lector");
        

        return ApiResult<bool>.BuildSuccess( rows >= 1);
    }
}

}
