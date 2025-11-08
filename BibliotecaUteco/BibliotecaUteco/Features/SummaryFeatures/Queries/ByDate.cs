using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Features.SummaryFeatures.Queries
{
    public class GetApplicationSummaryCommand : ICommand<IApiResult>
    {
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get; set; } = DateTime.UtcNow;

        public DateTime _startTime => StartTime.ToUniversalTime();
        public DateTime _endTime => EndTime.ToUniversalTime();
    }

    public class GetApplicationSummaryCommandValidator : AbstractValidator<GetApplicationSummaryCommand>
    {
        public GetApplicationSummaryCommandValidator()
        {
            RuleFor(x => x._endTime)
            .Must(x => x <= DateTime.UtcNow).WithMessage("No se puede obtener un reporte de una fecha mayor a la actual");
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
            ApplicationSummaryResponse response = new();

            var summary = await context.Books
             .GroupBy(_ => 1)
             .Select(_ => new ApplicationSummaryResponse
             {
                 StartTime = DateTime.Now, // opcional, no lo puedes hacer en SQL así que EF lo evaluará en memoria
                 EndTime = DateTime.Now,

                 BooksSummary = new BooksSummaryResponse
                 {
                     AvailableBooks = context.Books.Count(b => b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null && l.BookId == b.Id) >= 1),
                     LoanedBooks = context.Loans.Count(),
                     NonAvailableBooks = context.Books.Count(b => b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null && l.BookId == b.Id) <= 0),
                     TotalBooksCount = context.Books.Count()
                 },

                 LoansSummary = new LoanSummaryResponse
                 {
                     ActiveLoans = context.Loans.Count(l => l.ReturnedDate == null),
                     ReturnedLoans = context.Loans.Count(l => l.ReturnedDate != null),
                     ExceededLoans = context.Loans.Count(l => l.DueDate < DateTime.UtcNow && l.ReturnedDate == null),
                     TotalLoansCount = context.Loans.Count()
                 }
             })
             .FirstAsync();

            return new SuccessApiResult<ApplicationSummaryResponse>(summary);
        }
    }
}