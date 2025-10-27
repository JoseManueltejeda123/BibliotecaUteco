namespace BibliotecaUteco.Features.UserFeatures.Actions;

public class ResetPasswordCommand : CommandWithUserCredentials, ICommand<IApiResult>
{
    [FromBody, JsonPropertyName("userId"), Required, Range(1, int.MaxValue)]
    [Description("Id de usuario")]
    public int UserId { get; set; }
}

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("El Id del usuario debe de ser mayor a 0");
    }
}

internal class ResetPasswordEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
                EndpointSettings.UsersEndpoint + "/reset-password",
                async (
                    [FromBody] ResetPasswordCommand command,
                    ISender sender,
                    HttpContext context,
                    IEndpointWrapper<ResetPasswordEndpoint> wrapper,
                    CancellationToken cancellationToken = default
                ) =>
                {
                    return await wrapper.ExecuteAsync<IApiResult>(async () =>
                    {
                        command.SetCurrentUserId(
                            UserIdentityUtility.GetUserIdFromClaims(context.User)
                        );
                        return await sender.SendAndValidateAsync(command, cancellationToken);
                    });
                }
            )
            .RequireAuthorization(AuthorizationPolicies.AllowAdminsOnly)
            .RequireCors(CorsPolicies.DefaultPolicy)
            .Accepts<ResetPasswordCommand>(false, ApplicationContentTypes.ApplicationJson)
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

public class ResetPasswordCommandHandler(IBibliotecaUtecoDbContext context)
    : ICommandHandler<ResetPasswordCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(
        ResetPasswordCommand request,
        CancellationToken cancellationToken = default
    )
    {
        if (request.CurrentUserId == request.UserId)
        {
            return new ForbiddenApiResult(
                "No puedes restablecer tu propia contraseña de esta manera. Actualiza tus credenciales directamente"
            );
        }

        var user = await context.Users.FirstOrDefaultAsync(
            u => u.Id == request.UserId,
            cancellationToken
        );

        if (user == null)
        {
            return new NotFoundApiResult("Usuario no encontrado");
        }

        if (user.RoleId == 1)
        {
            return new ForbiddenApiResult(
                "No tienes permitido resetear la contraseña de un administrador"
            );
        }

        var resetedPassword = new string("Uteco.2025").Hash();
        user.Password = resetedPassword;
        await context.SaveChangesAsync(cancellationToken);

        context.ChangeTracker.Clear();

        return new SuccessApiResult<bool>(true);
    }
}
