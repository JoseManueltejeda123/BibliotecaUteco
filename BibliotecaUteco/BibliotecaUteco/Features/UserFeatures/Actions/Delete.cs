using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaUteco.Services;

namespace BibliotecaUteco.Features.UserFeatures.Actions
{
    public class DeleteUserCommand : CommandWithUserCredentials, ICommand<IApiResult>
    {
        [FromQuery(Name = "userId"), JsonPropertyName("userId"), Required]
        [Description("ID del usuario a eliminar")]
        public int UserId { get; set; }
    }

    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("El ID del usuario debe ser mayor a 0");
        }
    }

    internal class DeleteUserEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete(
                    EndpointSettings.UsersEndpoint + "/delete",
                    async (
                        [AsParameters] DeleteUserCommand command,
                        ISender sender,
                        HttpContext httpContext,
                        IEndpointWrapper<DeleteUserEndpoint> wrapper,
                        CancellationToken cancellationToken = default
                    ) =>
                    {
                        return await wrapper.ExecuteAsync<IApiResult>(async () =>
                        {
                            command.SetCurrentUserId(
                                UserIdentityUtility.GetUserIdFromClaims(httpContext.User)
                            );
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
                .Produces<InternalServerErrorApiResult>(
                    500,
                    ApplicationContentTypes.ApplicationJson
                )
                .WithTags(nameof(User))
                .WithName(nameof(DeleteUserEndpoint))
                .WithDescription(
                    "Elimina un usuario del sistema si no tiene transacciones asociadas"
                );
        }
    }

    public class DeleteUserCommandHandler(
        IBibliotecaUtecoDbContext context,
        IFileUploadService fileUploadService
    ) : ICommandHandler<DeleteUserCommand, IApiResult>
    {
        public async Task<IApiResult> HandleAsync(
            DeleteUserCommand request,
            CancellationToken cancellationToken = default
        )
        {
            if (
                await context.Users.FirstOrDefaultAsync(
                    u => u.Id == request.UserId,
                    cancellationToken
                )
                    is var user
                && user is null
            )
            {
                return new NotFoundApiResult($"No se encontró el usuario con ID {request.UserId}");
            }

            if (
                await context.Transactions.AnyAsync(
                    t => t.UserId == request.UserId,
                    cancellationToken
                )
            )
            {
                return new ConflictApiResult(
                    "No se puede eliminar el usuario porque tiene transacciones asociadas. "
                        + "Considere desactivar el usuario en lugar de eliminarlo."
                );
            }

            if (user.RoleId == (int)ApplicationRoles.Admin)
            {
                return new ForbiddenApiResult("No se puede eliminar un usuario administrador.");
            }

            if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
            {
                fileUploadService.DeleteFile(user.ProfilePictureUrl, EnvFolders.UserPictures);
            }

            // 5. Eliminar el usuario
            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);

            return new SuccessApiResult<bool>(true);
        }
    }
}
