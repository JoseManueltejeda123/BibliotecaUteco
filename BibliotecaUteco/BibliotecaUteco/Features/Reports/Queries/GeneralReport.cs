using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Features.Reports.Queries
{
    public class GetGeneralReportCommand : ICommand<IApiResult>
    {
        [FromQuery(Name = "year"), JsonPropertyName("year"), Required, Description("El año para obtener los datos")]
        public int Year {get; set;} = DateTime.UtcNow.Year;

         [FromQuery(Name = "month"), JsonPropertyName("month"), Description("El mes para obtener los datos")]
        public int? Month {get; set;} 


    }

public class GetGeneralReportCommandValidator : AbstractValidator<GetGeneralReportCommand>
{
    public GetGeneralReportCommandValidator()
    {
        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .WithMessage("El año debe estar entre 1900 y 2100");

        When(x => x.Month.HasValue, () =>
        {
            RuleFor(x => x.Month).Must(m => m <= 12).WithMessage("El mes debe de ser menor o igual a 12");
            
        });
    }
}

// Endpoint
internal class GetGeneralReportEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                EndpointSettings.ReportsEndpoint + "/general",
                async (
                    [AsParameters] GetGeneralReportCommand command,
                    ISender sender,
                    IEndpointWrapper<GetGeneralReportEndpoint> wrapper,
                    CancellationToken cancellationToken = default
                ) =>
                {
                    return await wrapper.ExecuteAsync<IApiResult>(async () =>
                    {
                        return await sender.SendAndValidateAsync(command, cancellationToken);
                    });
                }
            )
            .RequireAuthorization(AuthorizationPolicies.AllowAdminsOnly)
            .RequireCors(CorsPolicies.DefaultPolicy)
            .DisableAntiforgery()
            .Produces<SuccessApiResult<LoansPerMonthResponse>>(
                200,
                ApplicationContentTypes.ApplicationJson
            )
            .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)
            .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .Produces<InternalServerErrorApiResult>(500, ApplicationContentTypes.ApplicationJson)
            .WithName(nameof(GetGeneralReportEndpoint))
            .WithDescription("Obtiene un reporte general de la aplicacion");
    }
}

// Handler
public class GetGeneralReportCommandHandler(

    IBibliotecaUtecoDbContext context
)
    : ICommandHandler<GetGeneralReportCommand, IApiResult>
{
   
    public async Task<IApiResult> HandleAsync(
        GetGeneralReportCommand request,
        CancellationToken cancellationToken = default
    )
    {
       

        return new SuccessApiResult<GeneralReport>( request.Month.HasValue ? 
        await context.GetGeneralReportForAMonthAsync(request.Year, request.Month.Value, cancellationToken) : 
        await context.GetGeneralReportAsync(request.Year, cancellationToken) 
        
        );
    }
}
}