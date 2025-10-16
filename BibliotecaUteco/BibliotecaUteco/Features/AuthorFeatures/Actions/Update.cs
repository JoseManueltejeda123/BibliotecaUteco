namespace BibliotecaUteco.Features.AuthorFeatures.Actions;

public class UpdateAuthorCommand : ICommand<IApiResult>
    {
        [Range(1, int.MaxValue), Required, FromBody, JsonPropertyName("authorId")]
        [Description("El id del autor a actualizar")]
        public int AuthorId { get; set; }

        [MaxLength(50), MinLength(1), Required, FromBody, JsonPropertyName("fullName")]
        [Description("El nuevo nombre completo del autor")]
        public string FullName { get; set; } = null!;
    }

    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("El ID del autor debe ser mayor a 0");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("El nombre completo es obligatorio")
                .MinimumLength(1).WithMessage("El nombre debe tener al menos 1 carácter")
                .MaximumLength(50).WithMessage("El nombre no puede superar los 50 caracteres");
        }
    }

    internal class UpdateAuthorEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut(EndpointSettings.AuthorsEndpoint, async (
                    [FromBody] UpdateAuthorCommand command,
                    ISender sender,
                    IEndpointWrapper<UpdateAuthorEndpoint> wrapper,
                    CancellationToken cancellationToken = default
                ) =>
                {
                    return await wrapper.ExecuteAsync<IApiResult>(async () =>
                    {
                        return await sender.SendAndValidateAsync(command, cancellationToken);
                    });
                })
                .RequireAuthorization(AuthorizationPolicies.AllowAdminsOnly)
                .RequireCors(CorsPolicies.DefaultPolicy)
                .DisableAntiforgery()
                .Accepts<UpdateAuthorCommand>(false, ApplicationContentTypes.ApplicationJson)
                .Produces<ApiResult<AuthorResponse>>(200, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(409, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(403, ApplicationContentTypes.ApplicationJson)
                .WithTags(nameof(Author))
                .WithName(nameof(UpdateAuthorEndpoint))
                .WithDescription("Actualiza un autor existente y retorna el autor actualizado");
        }
    }

    public class UpdateAuthorCommandHandler(IBibliotecaUtecoDbContext context)
        : ICommandHandler<UpdateAuthorCommand, IApiResult>
    {
        public async Task<IApiResult> HandleAsync(UpdateAuthorCommand request, CancellationToken cancellationToken = default)
        {
            if(await context.Authors
                .FirstOrDefaultAsync(a => a.Id == request.AuthorId, cancellationToken) is var author && author is null) 
            {
                return ApiResult<AuthorResponse>.BuildFailure(HttpStatus.NotFound, "El autor no existe");
            }

            var normalizedName = request.FullName.NormalizeField();

            if(await context.Authors
                .AnyAsync(a => a.NormalizedFullName == normalizedName && a.Id != request.AuthorId, cancellationToken))
            {
                return ApiResult<AuthorResponse>.BuildFailure(
                    HttpStatus.Conflict,
                    "Ya existe otro autor con ese nombre");
            }

            author.Update(request);
            await context.SaveChangesAsync(cancellationToken);
            context.ChangeTracker.Clear();
            return ApiResult<AuthorResponse>.BuildSuccess(author.ToResponse());
        }
    }