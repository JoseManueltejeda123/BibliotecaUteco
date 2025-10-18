using BibliotecaUteco.Client.Settings;
using BibliotecaUteco.Services;

namespace BibliotecaUteco.Features.UserFeatures.Actions;

 public class CreateUserCommand : ICommand<IApiResult>
    {
        [FromBody, JsonPropertyName("fullName"), Required, MaxLength(50), MinLength(5)]
        [Description("Nombre completo del usuario")]
        public string FullName { get; set; } = null!;

        [FromBody, JsonPropertyName("username"), Required, MaxLength(15), MinLength(5)]
        [Description("Nombre de usuario (solo letras, números, . y _)")]
        [RegularExpression(@"^[a-zA-Z0-9._]+$")]
        public string Username { get; set; } = null!;

        [FromBody, JsonPropertyName("password"), Required, MinLength(8)]
        [Description("Contraseña (mínimo 8 caracteres)")]
        public string Password { get; set; } = null!;

        [FromBody, JsonPropertyName("identityCardNumber"), Required, MaxLength(11), MinLength(11)]
        [Description("Cédula (11 dígitos)")]
        public string IdentityCardNumber { get; set; } = null!;

        [FromBody, JsonPropertyName("roleId"), Required, Range(1, 2)]
        [Description("ID del rol")]
        public int RoleId { get; set; }
        
        [FromBody, JsonPropertyName("sexId"), Required, Range(1, 2)]
        [Description("ID del sexo")]
        public int SexId { get; set; }

        [FromForm(Name = "profilePictureFile"), JsonPropertyName("profilePictureFile")]
        [Description("Foto de perfil (opcional)")]
        public IFormFile? ProfilePictureFile { get; set; }
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("El nombre completo es requerido")
                .MinimumLength(5).WithMessage("El nombre debe tener al menos 5 caracteres")
                .MaximumLength(50).WithMessage("El nombre no puede superar los 50 caracteres");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario es requerido")
                .MinimumLength(5).WithMessage("El nombre de usuario debe tener al menos 5 caracteres")
                .MaximumLength(15).WithMessage("El nombre de usuario no puede superar los 15 caracteres")
                .Matches(@"^[a-zA-Z0-9._]+$")
                .WithMessage("El nombre de usuario solo puede contener letras, números, puntos y guiones bajos");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
                .Matches(@"[A-Z]").WithMessage("La contraseña debe contener al menos una mayúscula")
                .Matches(@"[a-z]").WithMessage("La contraseña debe contener al menos una minúscula")
                .Matches(@"[0-9]").WithMessage("La contraseña debe contener al menos un número");

            RuleFor(x => x.IdentityCardNumber)
                .NotEmpty().WithMessage("La cédula es requerida")
                .Length(11).WithMessage("La cédula debe tener exactamente 11 dígitos")
                .Matches(@"^\d{11}$").WithMessage("La cédula solo puede contener números");

            RuleFor(x => x.RoleId)
                .GreaterThan(0).WithMessage("Debe seleccionar un rol válido");
            
            RuleFor(x => x.SexId)
                .GreaterThan(0).WithMessage("Debe seleccionar un sexo valido.");

            When(x => x.ProfilePictureFile != null, () =>
            {
                RuleFor(x => x.ProfilePictureFile!.Length)
                    .Must(x => x <= FilesSettings.MaxFileSize)
                    .WithMessage("La portada no puede superar los 2MB");

                RuleFor(x => x.ProfilePictureFile!.ContentType)
                    .Must(x => FilesSettings.AllowedImageExtensionsForUpload.Contains(x))
                    .WithMessage("La foto de perfil debe ser una imagen válida (JPG, PNG, WEBP)");
            });
        }
    }

    internal class CreateUserEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost(EndpointSettings.UsersEndpoint, async (
                    [FromForm] CreateUserCommand command,
                    ISender sender,
                    IEndpointWrapper<CreateUserEndpoint> wrapper,
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
                .Produces<ApiResult<UserResponse>>(200, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(409, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(403, ApplicationContentTypes.ApplicationJson)
                .WithTags(nameof(User))
                .WithName(nameof(CreateUserEndpoint))
                .WithDescription("Crea un nuevo usuario en el sistema");
        }
    }

    public class CreateUserCommandHandler(
        IBibliotecaUtecoDbContext context,
        IFileUploadService fileUploadService)
        : ICommandHandler<CreateUserCommand, IApiResult>
    {
        public async Task<IApiResult> HandleAsync(CreateUserCommand request, CancellationToken cancellationToken = default)
        {
            if (await context.Users.AnyAsync(u => u.Username == request.Username, cancellationToken))
            {
                return ApiResult<UserResponse>.BuildFailure(HttpStatus.Conflict, "El nombre de usuario ya existe");
            }

            if (await context.Users.AnyAsync(u => u.IdentityCardNumber == request.IdentityCardNumber, cancellationToken))
            {
                return ApiResult<UserResponse>.BuildFailure(HttpStatus.Conflict, "La cédula ya está registrada");
            }

            if (!await context.Roles.AnyAsync(r => r.Id == request.RoleId, cancellationToken))
            {
                return ApiResult<UserResponse>.BuildFailure(HttpStatus.BadRequest, "El rol especificado no existe");
            }

            var hashed = request.Password.Hash();
            request.Password = hashed;
            var insertion = await context.Users.AddAsync(User.Create(request), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            // 6. Subir foto de perfil si existe
            if (request.ProfilePictureFile != null && insertion.Entity.Id != 0)
            {
                if (await fileUploadService.UploadImageAsync(
                        request.ProfilePictureFile,
                        EnvFolders.UserPictures,
                        insertion.Entity.Id.ToString()) is var result && !result.Item1)
                {
                    return ApiResult<UserResponse>.BuildFailure(HttpStatus.BadRequest, result.Item2);
                }
                
                
                insertion.Entity.ProfilePictureUrl = result.Item2;
                await context.SaveChangesAsync(cancellationToken);
                
            }

            context.ChangeTracker.Clear();

            var createdUser = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == insertion.Entity.Id, cancellationToken);

            if (createdUser == null)
            {
                return ApiResult<UserResponse>.BuildFailure(HttpStatus.BadRequest, "Error al crear el usuario");
            }

            return ApiResult<UserResponse>.BuildSuccess(createdUser.ToResponse());
        }
    }