using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Features.UserFeatures.Actions
{
    public class ChangeUserStateCommand : ICommand<IApiResult>
    {
        [FromBody, JsonPropertyName("userId"), Required]
        [Description("ID del usuario")]
        public int UserId { get; set; }

        [FromBody, JsonPropertyName("isDisabled"), Required]
        [Description("Estado del usuario (true = deshabilitado, false = habilitado)")]
        public bool IsDisabled { get; set; }
    }

    public class ChangeUserStateCommandValidator : AbstractValidator<ChangeUserStateCommand>
    {
        public ChangeUserStateCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("El ID del usuario debe ser mayor a 0");

            RuleFor(x => x.IsDisabled)
                .NotNull()
                .WithMessage("Debe especificar el estado del usuario");
        }
    }

    internal class ChangeUserStateEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut(
                    EndpointSettings.UsersEndpoint + "/change-state",
                    async (
                        [FromBody] ChangeUserStateCommand command,
                        ISender sender,
                        IEndpointWrapper<ChangeUserStateEndpoint> wrapper,
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
                .Produces<SuccessApiResult<UserResponse>>(
                    200,
                    ApplicationContentTypes.ApplicationJson
                )
                .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)
                .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)
                .Produces<ForbiddenApiResult>(403, ApplicationContentTypes.ApplicationJson)
                .Produces<InternalServerErrorApiResult>(
                    500,
                    ApplicationContentTypes.ApplicationJson
                )
                .WithTags(nameof(User))
                .WithName(nameof(ChangeUserStateEndpoint))
                .WithDescription("Habilita o deshabilita un usuario en el sistema");
        }
    }

    public class ChangeUserStateCommandHandler(IBibliotecaUtecoDbContext context)
        : ICommandHandler<ChangeUserStateCommand, IApiResult>
    {
        public async Task<IApiResult> HandleAsync(
            ChangeUserStateCommand request,
            CancellationToken cancellationToken = default
        )
        {
            // 1. Verificar que el usuario existe
            var user = await context.Users.FirstOrDefaultAsync(
                u => u.Id == request.UserId,
                cancellationToken
            );

            if (user == null)
            {
                return new NotFoundApiResult($"No se encontró el usuario con ID {request.UserId}");
            }

            // 2. Verificar si el estado ya es el mismo
            if (user.IsDisabled == request.IsDisabled)
            {
                string currentState = user.IsDisabled ? "deshabilitado" : "habilitado";
                return new BadRequestApiResult(
                    $"El usuario '{user.Username}' ya se encuentra {currentState}"
                );
            }

            user.IsDisabled = request.IsDisabled;
            await context.SaveChangesAsync(cancellationToken);
            context.ChangeTracker.Clear();

            string action = request.IsDisabled ? "deshabilitado" : "habilitado";
            return new SuccessApiResult<UserResponse>(
                user.ToResponse(),
                $"Usuario '{user.Username}' {action} correctamente"
            );
        }
    }
}
