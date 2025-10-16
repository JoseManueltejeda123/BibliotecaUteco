namespace BibliotecaUteco.Features.AuthorFeatures.Actions;

public class CreateAuthorCommand : ICommand<IApiResult>
{
    [MaxLength(50), MinLength(1), Required, FromBody, JsonPropertyName("fullName"), Description("El nombre completo del autor")]
        public string FullName { get; set; } = null!;
}


public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("El nombre completo del autor es obligatorio.")
            .MinimumLength(1)
            .WithMessage("El nombre completo del autor debe tener al menos 1 carácter.")
            .MaximumLength(50)
            .WithMessage("El nombre completo del autor no puede superar los 50 caracteres.");
    }
}

internal class CreateAuthorEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(EndpointSettings.AuthorsEndpoint, async (
                [FromBody] CreateAuthorCommand command,
                ISender sender,
                IEndpointWrapper<CreateAuthorEndpoint> wrapper,
                CancellationToken cancellationToken = default
            ) =>
            {
                return await wrapper.ExecuteAsync<IApiResult>(async () =>
                    {

                        return await sender.SendAndValidateAsync(command, cancellationToken);
                    }
                );
            })
            .RequireAuthorization(AuthorizationPolicies.AllowAdminsOnly)
            .DisableAntiforgery()
            .RequireCors()
            .Accepts<CreateAuthorCommand>(false, ApplicationContentTypes.ApplicationJson)
            .Produces<ApiResult<AuthorResponse>>(200, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Author))
            .WithName(nameof(CreateAuthorEndpoint))
            .WithDescription($"Crea un autor en la base de datos y retorna un  {nameof(IApiResult)} con un {nameof(AuthorResponse)}");


    }
}


public class AuthenticateUserCommandHandler(IBibliotecaUtecoDbContext context) : ICommandHandler<CreateAuthorCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(CreateAuthorCommand request, CancellationToken cancellationToken = default)
    {

        var normalizedName = request.FullName.NormalizeField();
        if (await context.Authors.AnyAsync(a => a.NormalizedFullName == normalizedName, cancellationToken))
        {
            return ApiResult<AuthorResponse>.BuildFailure(HttpStatus.Conflict, "Ya existe este autor/a con este mismo nombre");
        }

        await context.Authors.AddAsync(Author.Create(request), cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        context.ChangeTracker.Clear();

        var authors = await context.Authors.GetAuthorsByName(request.FullName, 1, cancellationToken);

        if (authors.FirstOrDefault() is null)
            return ApiResult<AuthorResponse>.BuildFailure(HttpStatus.BadRequest,
                "No pudimos obtener el autor recien creado");

        return ApiResult<AuthorResponse>.BuildSuccess(authors.FirstOrDefault()?.ToResponse());

    }
}
