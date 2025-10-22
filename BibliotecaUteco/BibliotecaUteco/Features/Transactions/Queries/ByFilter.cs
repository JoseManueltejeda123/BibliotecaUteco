namespace BibliotecaUteco.Features.Transactions.Queries;

public class GetTransactionsByFilterCommand : ICommand<IApiResult>
{
    [FromQuery(Name = "userId"), JsonPropertyName("userId"), Description("El ID del usuario que hizo la transacción"),
     Range(0, int.MaxValue)]
        public int? UserId { get; set; } = null;
    [FromQuery(Name = "skip"), JsonPropertyName("skip"), Description("Cantidad de transacciones a omitir"),
     Range(0, int.MaxValue)]
    public int? Skip { get; set; } = 0;
    
       [FromQuery(Name = "take"), JsonPropertyName("take"), Description("Cantidad de transacciones a tomar"),
         Range(1, 10)]
    public int? Take { get; set; } = 10;
}

public class GetTransactionsByFilterCommandValidator : AbstractValidator<GetTransactionsByFilterCommand>
{
    public GetTransactionsByFilterCommandValidator()
    {
       
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .When(x => x.UserId.HasValue).WithMessage("El ID del usuario para filtrar las transacciones debe de ser mayor a cero. ");
        
        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0)
            .When(x => x.Skip.HasValue).WithMessage("La cantidad de transacciones a omitir debe de ser mayor o igual a 0 ");
        
        RuleFor(x => x.Take)
                    .InclusiveBetween(1, 10)
                    .When(x => x.Take.HasValue).WithMessage("La cantidad de transacciones a tomar debe de ser entre 1 y 10");
    }
}

public class GetTransactionsByFilterEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(EndpointSettings.TransactionsEndpoint + "/by-filter", async ( 
                [AsParameters] GetTransactionsByFilterCommand command,
                IEndpointWrapper<GetTransactionsByFilterEndpoint> endpointWrapper,
                ISender sender,
                CancellationToken token = default
                ) =>
                {
                    return await endpointWrapper.ExecuteAsync<IApiResult>(async () =>
                    {
                        return await sender.SendAndValidateAsync(command, token);
                    });
                }
            ).RequireAuthorization(AuthorizationPolicies.AllowAdminsOnly)
            .RequireCors()
            .DisableAntiforgery()
            .Produces<ApiResult<List<TransactionResponse>>>(200, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Transaction))
            .WithName(nameof(GetTransactionsByFilterEndpoint))
            .WithDescription("Busca una lista de transacciones");
    }
}


internal class GetTransactionsByFilterCommandHandler(IBibliotecaUtecoDbContext context) : ICommandHandler<GetTransactionsByFilterCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(GetTransactionsByFilterCommand request, CancellationToken cancellationToken = default)
    {
        var result = await context.Transactions.GetByFilterAsync(request.UserId, request.Skip ?? 0, request.Take ?? 0,
            cancellationToken);

        return ApiResult<List<TransactionResponse>>.BuildSuccess(result.Select(t => t.ToResponse()).ToList());
    }
}