namespace BibliotecaUteco.Features.UserFeatures.Actions;
public class ResetPasswordCommand : ICommand<IApiResult>
{
    [FromBody, JsonPropertyName("username"), Required, MaxLength(15), MinLength(5)]
    [Description("Nombre de usuario")]
    [RegularExpression(@"^[a-zA-Z0-9._]+$")]
    public string Username { get; set; } = null!;
}

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("El nombre de usuario es requerido")
            .MinimumLength(5)
            .WithMessage("El nombre de usuario debe tener al menos 5 caracteres")
            .MaximumLength(15)
            .WithMessage("El nombre de usuario no puede superar los 15 caracteres")
            .Matches(@"^[a-zA-Z0-9._]+$")
            .WithMessage(
                "El nombre de usuario solo puede contener letras, números, puntos y guiones bajos"
            );
    }
}

internal class ResetPasswordEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                $"{EndpointSettings.UsersEndpoint}/reset-password",
                async (
                    [FromBody] ResetPasswordCommand command,
                    ISender sender,
                    IEndpointWrapper<ResetPasswordEndpoint> wrapper,
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
            .Produces<SuccessApiResult<bool>>(200, ApplicationContentTypes.ApplicationJson)
            .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)
            .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .Produces<ForbiddenApiResult>(403, ApplicationContentTypes.ApplicationJson)
            .Produces<InternalServerErrorApiResult>(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(User))
            .WithName(nameof(ResetPasswordEndpoint))
            .WithDescription("Reinicia la contraseña de un usuario a su cédula");
    }
}

public class ResetPasswordCommandHandler(
    IBibliotecaUtecoDbContext context
) : ICommandHandler<ResetPasswordCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(
        ResetPasswordCommand request,
        CancellationToken cancellationToken = default
    )
    {
        var user = await context
            .Users
            .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

        if (user == null)
        {
            return new NotFoundApiResult("Usuario no encontrado");
        }

        if (user.RoleId == 1)
        {
            return new ForbiddenApiResult("No tienes permitido resetear la contraseña de un administrador");
        }

        var resetedPassword = new string("Uteco.2025").Hash();
        user.Password = resetedPassword;
        await context.SaveChangesAsync(cancellationToken);

        context.ChangeTracker.Clear();


        return new SuccessApiResult<bool>(true);
    }
}