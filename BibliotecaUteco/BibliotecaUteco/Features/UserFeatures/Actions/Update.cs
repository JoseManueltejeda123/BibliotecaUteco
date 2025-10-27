using BibliotecaUteco.Client.Settings;
using BibliotecaUteco.Features;
using BibliotecaUteco.Features.UserFeatures.Actions;
using BibliotecaUteco.Services;

namespace BibliotecaUteco.Features.UserFeatures.Actions
{
    public class UpdateUserCommand : CommandWithUserCredentials, ICommand<IApiResult>
    {
        [FromForm(Name = "userId"), JsonPropertyName("userId"), Required, Range(1, int.MaxValue)]
        [Description("Id del usuario a actualizar")]
        public int UserId { get; set; }

        [
            FromForm(Name = "fullName"),
            JsonPropertyName("fullName"),
            Required,
            MaxLength(50),
            MinLength(5)
        ]
        [Description("Nombre completo del usuario")]
        public string FullName { get; set; } = null!;

        [
            FromForm(Name = "userName"),
            JsonPropertyName("username"),
            Required,
            MaxLength(15),
            MinLength(5)
        ]
        [Description("Nombre de usuario (solo letras, números, . y _)")]
        [RegularExpression(@"^[a-zA-Z0-9._]+$")]
        public string Username { get; set; } = null!;

        [
            FromForm(Name = "identityCardNumber"),
            JsonPropertyName("identityCardNumber"),
            Required,
            MaxLength(11),
            MinLength(11)
        ]
        [Description("Cédula (11 dígitos)")]
        public string IdentityCardNumber { get; set; } = null!;

        [
            FromForm(Name = "currentPassword"),
            JsonPropertyName("currentPassword"),
            MaxLength(30),
            MinLength(8)
        ]
        [Description("Contraseña actual (mínimo 8 caracteres)")]
        public string? CurrentPassword { get; set; }

        [
            FromForm(Name = "newPassword"),
            JsonPropertyName("newPassword"),
            MaxLength(30),
            MinLength(8)
        ]
        [Description("Contraseña actual (mínimo 8 caracteres)")]
        public string? NewPassword { get; set; }

        [FromForm(Name = "sexId"), JsonPropertyName("sexId"), Required, Range(1, 2)]
        [Description("ID del sexo")]
        public int SexId { get; set; }

        [FromForm(Name = "profilePictureFile"), JsonPropertyName("profilePictureFile")]
        [Description("Foto de perfil (opcional)")]
        public IFormFile? ProfilePictureFile { get; set; }

        [
            FromForm(Name = "removeProfilePicture"),
            JsonPropertyName("removeProfilePicture"),
            Description("Indica si se debe de eliminar la foto de perfil del usuario"),
            Required
        ]
        public bool RemoveProfilePicture { get; set; } = false;
    }
}

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("El nombre completo es requerido")
            .MinimumLength(5)
            .WithMessage("El nombre debe tener al menos 5 caracteres")
            .MaximumLength(50)
            .WithMessage("El nombre no puede superar los 50 caracteres");

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

        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithMessage("La contraseña actual es requerida")
            .MaximumLength(30)
            .WithMessage("La contraseña actual debe de tener maximo 30 caracteres")
            .MinimumLength(8)
            .WithMessage("La contraseña actual debe tener al menos 8 caracteres")
            .When(x => !string.IsNullOrEmpty(x.CurrentPassword));

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("La contraseña nueva es requerida")
            .MaximumLength(30)
            .WithMessage("La contraseña nueva debe de tener maximo 30 caracteres")
            .MinimumLength(8)
            .WithMessage("La contraseña nueva debe tener al menos 8 caracteres")
            .Matches(@"[A-Z]")
            .WithMessage("La contraseña nueva debe contener al menos una mayúscula")
            .Matches(@"[a-z]")
            .WithMessage("La contraseña nueva debe contener al menos una minúscula")
            .Matches(@"[0-9]")
            .WithMessage("La contraseña nueva debe contener al menos un número")
            .When(x => !string.IsNullOrEmpty(x.CurrentPassword));

        RuleFor(x => x.IdentityCardNumber)
            .NotEmpty()
            .WithMessage("La cédula es requerida")
            .Length(11)
            .WithMessage("La cédula debe tener exactamente 11 dígitos")
            .Matches(@"^\d{11}$")
            .WithMessage("La cédula solo puede contener números");

        RuleFor(x => x.SexId).GreaterThan(0).WithMessage("Debe seleccionar un sexo valido.");

        When(
            x => x.ProfilePictureFile != null,
            () =>
            {
                RuleFor(x => x.ProfilePictureFile!.Length)
                    .Must(x => x <= FilesSettings.MaxFileSize)
                    .WithMessage("La portada no puede superar los 2MB");

                RuleFor(x => x.ProfilePictureFile!.ContentType)
                    .Must(x => FilesSettings.AllowedImageExtensionsForUpload.Contains(x))
                    .WithMessage("La foto de perfil debe ser una imagen válida (JPG, PNG, WEBP)");
            }
        );
    }
}

internal class UpdateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
                EndpointSettings.UsersEndpoint,
                async (
                    [FromForm] UpdateUserCommand command,
                    HttpContext context,
                    ISender sender,
                    IEndpointWrapper<UpdateUserEndpoint> wrapper,
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
            .RequireAuthorization(AuthorizationPolicies.AllowAuthorizedUsers)
            .RequireCors(CorsPolicies.DefaultPolicy)
            .DisableAntiforgery()
            .Produces<SuccessApiResult<UserResponse>>(200, ApplicationContentTypes.ApplicationJson)
            .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(409, ApplicationContentTypes.ApplicationJson)
            .Produces<InternalServerErrorApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .Produces<ForbiddenApiResult>(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(User))
            .WithName(nameof(UpdateUserEndpoint))
            .WithDescription("Actualiza un nuevo usuario en el sistema");
    }
}

public class UpdateUserCommandHandler(
    IBibliotecaUtecoDbContext context,
    IFileUploadService fileUploadService
) : ICommandHandler<UpdateUserCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(
        UpdateUserCommand request,
        CancellationToken cancellationToken = default
    )
    {
        if (
            await context.Users.AnyAsync(
                u => u.Username == request.Username && u.Id != request.UserId,
                cancellationToken
            )
        )
        {
            return new ConflictApiResult("Ya hay un usuario con ese nombre de usuario");
        }

        if (
            await context.Users.AnyAsync(
                u => u.IdentityCardNumber == request.IdentityCardNumber && u.Id != request.UserId,
                cancellationToken
            )
        )
        {
            return new ConflictApiResult("Ya hay un usuario con esa cédula");
        }
        var user = await context.Users.FirstOrDefaultAsync(
            u => u.Id == request.UserId,
            cancellationToken
        );

        if (user is null)
        {
            return new NotFoundApiResult("No existe el usuario a actualizar");
        }

        if (!string.IsNullOrEmpty(request.CurrentPassword))
        {
            var currentHashed = request.CurrentPassword.Hash();

            if (user.Password != currentHashed)
            {
                return new BadRequestApiResult("La contraseña actual no coinciden");
            }
        }

        user.Update(request);

        if (request.RemoveProfilePicture && !string.IsNullOrEmpty(user.ProfilePictureUrl))
        {
            var oldPfp = user.ProfilePictureUrl;

            user.ProfilePictureUrl = null;
            await context.SaveChangesAsync(cancellationToken);
            if (
                fileUploadService.DeleteFile(oldPfp, EnvFolders.UserPictures) is var deletionResult
                && !deletionResult.Item1
            )
            {
                return new BadRequestApiResult(
                    $"No pudimos eliminar la foto de perfil: {deletionResult.Item2}"
                );
            }
        }
        else if (request.ProfilePictureFile is not null)
        {
            if (
                await fileUploadService.UploadImageAsync(
                    request.ProfilePictureFile,
                    EnvFolders.UserPictures,
                    user.Id.ToString()
                )
                    is var result
                && !result.Item1
            )
            {
                return new BadRequestApiResult(
                    $"No pudimos subir la foto de perfil: {result.Item2}"
                );
            }

            user.ProfilePictureUrl = result.Item2;
        }

        await context.SaveChangesAsync(cancellationToken);
        context.ChangeTracker.Clear();

        return new SuccessApiResult<UserResponse>(user.ToResponse());
    }
}
