namespace BibliotecaUteco.Features.AuthorFeatures.Actions;

public class DeleteAuthorCommand : ICommand<IApiResult>
{
    [Range(1, int.MaxValue), Required, FromQuery(Name = "authorId"), JsonPropertyName("authorId")]
    [Description("El id del autor a eliminar")]
    public int AuthorId { get; set; }
}

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(x => x.AuthorId).GreaterThan(0).WithMessage("El ID del autor debe ser mayor a 0");
    }
}

internal class DeleteAuthorEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                EndpointSettings.AuthorsEndpoint + "/delete",
                async (
                    [AsParameters] DeleteAuthorCommand command,
                    ISender sender,
                    IEndpointWrapper<DeleteAuthorEndpoint> wrapper,
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
            .Produces<SuccessApiResult<bool>>(200, ApplicationContentTypes.ApplicationJson)
            .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)
            .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .Produces<ConflictApiResult>(409, ApplicationContentTypes.ApplicationJson)
            .Produces<InternalServerErrorApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .Produces<ForbiddenApiResult>(403, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Author))
            .WithName(nameof(DeleteAuthorEndpoint))
            .WithDescription("Elimina un autor. No se puede eliminar si tiene libros asociados");
    }
}

public class DeleteAuthorCommandHandler(IBibliotecaUtecoDbContext context)
    : ICommandHandler<DeleteAuthorCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(
        DeleteAuthorCommand request,
        CancellationToken cancellationToken = default
    )
    {
        var rows = await context
            .Authors.Where(a => a.Id == request.AuthorId)
            .ExecuteDeleteAsync(cancellationToken);

        return new SuccessApiResult<bool>(rows >= 1);
    }
}
