using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Features.SummaryFeatures.Queries
{
    public class GetApplicationSummaryCommand : ICommand<IApiResult>
    {
        public DateTime? Date { get; set; }
        public bool IsPrecise { get; set; } = false;
       
    }

    public class GetApplicationSummaryCommandValidator : AbstractValidator<GetApplicationSummaryCommand>
    {
        public GetApplicationSummaryCommandValidator()
        {
            RuleFor(x => x.Date)
            .Must(x => x <= DateTime.UtcNow).WithMessage("No se puede obtener un reporte de una fecha mayor a la actual").When(x => x.Date.HasValue);
        }
    }

    public class GetApplicationSummaryEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet(EndpointSettings.SummarysEndpoint + "/summary",
            async (
                [AsParameters] GetApplicationSummaryCommand command,
                ISender sender,
                IEndpointWrapper<GetApplicationSummaryEndpoint> wrapper,
                CancellationToken token = default
            ) =>
            {
                return await wrapper.ExecuteAsync<IApiResult>(async () =>
                {
                    return await sender.SendAndValidateAsync(command, token);

                });

            })
            .RequireAuthorization(AuthorizationPolicies.AllowAdminsOnly)
            .RequireCors(CorsPolicies.DefaultPolicy)
            .DisableAntiforgery()
            .Produces<SuccessApiResult<ApplicationSummaryResponse>>(
                200,
                ApplicationContentTypes.ApplicationJson
            )
            .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)
            .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .Produces<InternalServerErrorApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .WithName(nameof(GetApplicationSummaryEndpoint))
            .WithDescription("Retorna un resumen de la aplicacion");
        }
    }

    internal class GetApplicationSummaryCommandHandler(
        IBibliotecaUtecoDbContext context

    ) : ICommandHandler<GetApplicationSummaryCommand, IApiResult>
    {
        public async Task<IApiResult> HandleAsync(GetApplicationSummaryCommand request, CancellationToken cancellationToken = default)
        {
            var response = await context.GetApplicationSummaryResponseAsync(request.Date);

            return new SuccessApiResult<ApplicationSummaryResponse>(response);
        }
    }
}