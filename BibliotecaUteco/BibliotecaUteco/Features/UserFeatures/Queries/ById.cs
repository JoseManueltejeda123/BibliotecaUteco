namespace BibliotecaUteco.Features.UserFeatures.Queries;

public class GetUserByIdCommand : ICommand<IApiResult>
{
    [FromQuery(Name = "userId"), JsonPropertyName("userId"), Range(1, int.MaxValue)]
    [Description("Id de usuario a buscar")]
    public int UserId { get; set; }
}

public class GetUserByIdCommandValidator : AbstractValidator<GetUserByIdCommand>
{
    public GetUserByIdCommandValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("El id de usuario debe de ser mayor a 0");
    }
}

internal class GetUserByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                EndpointSettings.UsersEndpoint + "/by-id",
                async (
                    [AsParameters] GetUserByIdCommand query,
                    ISender sender,
                    IEndpointWrapper<GetUserByNameEndpoint> wrapper,
                    CancellationToken cancellationToken = default
                ) =>
                {
                    return await wrapper.ExecuteAsync<IApiResult>(async () =>
                    {
                        return await sender.SendAndValidateAsync(query, cancellationToken);
                    });
                }
            )
            .RequireAuthorization(AuthorizationPolicies.AllowAuthorizedUsers)
            .RequireCors(CorsPolicies.DefaultPolicy)
            .DisableAntiforgery()
            .Produces<SuccessApiResult<UserResponse>>(200, ApplicationContentTypes.ApplicationJson)
            .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .Produces<InternalServerErrorApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(User))
            .WithName(nameof(GetUserByIdEndpoint))
            .WithDescription("Retorna un usuario por su id");
    }
}

public class GetUserByIdCommandHandler(IBibliotecaUtecoDbContext context)
    : ICommandHandler<GetUserByIdCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(
        GetUserByIdCommand request,
        CancellationToken cancellationToken = default
    )
    {
        if (
            await context.Users.GetByIdAsync(request.UserId, cancellationToken) is var user
            && user is null
        )
        {
            return new NotFoundApiResult("Usuario no encontrado");
        }

        return new SuccessApiResult<UserResponse>(user.ToResponse());
    }
}
