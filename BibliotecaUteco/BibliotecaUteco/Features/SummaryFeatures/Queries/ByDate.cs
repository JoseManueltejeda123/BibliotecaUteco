using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Features.SummaryFeatures.Queries
{
    public class GetApplicationSummaryCommand : ICommand<IApiResult>
    {
        [FromQuery(Name = "date"), JsonPropertyName("date"), Description("La fecha precisa de donde quieres los reportes (opcional)")]
        public DateTime? Date { get; set; }
        
        [FromQuery(Name = "isPrecise"), JsonPropertyName("isPrecise"), Description("Si se establece como verdadero, los reportes se harán de solo esa fecha, de lo contrario, se obtendrán los datos menor o igual a esa fecha (opcional)")]
        public bool? IsPrecise { get; set; } = false;
       
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
            app.MapGet(EndpointSettings.SummarysEndpoint + "/by-date",
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
            .AllowAnonymous()
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
           

            return new SuccessApiResult<ApplicationSummaryResponse>(
                
                request.IsPrecise.HasValue && request.IsPrecise.Value ?  
                    await context.GetApplicationSummaryResponsePreciseAsync(request.Date, cancellationToken) :
                    await context.GetApplicationSummaryResponseAsync(request.Date, cancellationToken) 

            );
        }
    }
}