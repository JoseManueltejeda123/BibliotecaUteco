namespace BibliotecaUteco.Features.GenreFeatures.Actions
{
       
    
    
    public class CreateGenreCommand : ICommand<IApiResult>
    {
        [MaxLength(25)]
        [MinLength(1)]
        [Required]
        [FromBody]
        [Description("El nombre del genero a crear")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
    }
    
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(1).WithMessage("El nombre del genero literario debe tener al menos 1 carácter.")
                .MaximumLength(25).WithMessage("El nombre del genero literario no puede tener más de 25 caracteres.");
        }
    }
    
    internal class CreateGenreEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost(EndpointSettings.GenresEndpoint, async (
                    [FromBody] CreateGenreCommand command,
                    ISender sender,
                    IEndpointWrapper<CreateGenreEndpoint> wrapper,
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
                .Accepts<CreateGenreCommand>(false, ApplicationContentTypes.ApplicationJson)
                .Produces<ApiResult<GenreResponse>>(200, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(403, ApplicationContentTypes.ApplicationJson)
                .WithTags(nameof(Genre))
                .WithName(nameof(CreateGenreEndpoint))
                .WithDescription($"Crea un nuevo género literario y retorna un {nameof(IApiResult)} con el género creado");
        }
    }
    
    public class CreateGenreCommandHandler(IBibliotecaUtecoDbContext context) : ICommandHandler<CreateGenreCommand, IApiResult>
    {
        public async Task<IApiResult> HandleAsync(CreateGenreCommand request, CancellationToken cancellationToken = default)
        {
            var normalizedName = request.Name.NormalizeField();
            if (await context.Genres.AnyAsync(g => g.NormalizedName == normalizedName, cancellationToken))
            {
                return ApiResult<GenreResponse>.BuildFailure(HttpStatus.Conflict, "Ya existe un género con ese nombre.");
            }
    
           
    
            await context.Genres.AddAsync(Genre.Create(request), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            context.ChangeTracker.Clear();

            if(await context.Genres.FirstOrDefaultAsync(g => g.NormalizedName == normalizedName) is var genre && genre is null)
            {
                return ApiResult<GenreResponse>.BuildFailure(HttpStatus.BadRequest, "El genero no pudo ser encontrado");
            }
    
            return ApiResult<GenreResponse>.BuildSuccess(genre.ToResponse());
        }
    }}