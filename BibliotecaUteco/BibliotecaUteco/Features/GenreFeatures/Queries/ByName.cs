namespace BibliotecaUteco.Features.GenreFeatures.Queries
{
    public class GetGenresByName : ICommand<IApiResult>
    {
        [JsonPropertyName("genreName")]
        [FromQuery(Name = "genreName")]
        [MaxLength(25)]
        [Description("El nombre del genero a buscar")]
        public string? GenreName { get; set; }
    }

    public class GetGenresByNameValidator : AbstractValidator<GetGenresByName>
    {
        public GetGenresByNameValidator()
        {
            RuleFor(x => x.GenreName)
                .MaximumLength(25)
                .WithMessage("El nombre del género no puede tener más de 25 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.GenreName));
        }
    }

    internal class GetGenresByNameEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet(
                    EndpointSettings.GenresEndpoint + "/by-name",
                    async (
                        [AsParameters] GetGenresByName command,
                        ISender sender,
                        IEndpointWrapper<GetGenresByNameEndpoint> wrapper,
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
                .Produces<SuccessApiResult<List<GenreResponse>>>(
                    200,
                    ApplicationContentTypes.ApplicationJson
                )
                .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)
                .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)
                .Produces<InternalServerErrorApiResult>(
                    500,
                    ApplicationContentTypes.ApplicationJson
                )
                .WithTags(nameof(Genre))
                .WithName(nameof(GetGenresByNameEndpoint))
                .WithDescription(
                    $"Retorna una lista de 5 géneros según el nombre dado {nameof(IApiResult)}"
                );
        }
    }

    public class GetGenresByNameHandler(IBibliotecaUtecoDbContext context)
        : ICommandHandler<GetGenresByName, IApiResult>
    {
        public async Task<IApiResult> HandleAsync(
            GetGenresByName request,
            CancellationToken cancellationToken = default
        )
        {
            var normalizedName = request.GenreName?.NormalizeField() ?? "";
            var genres = await context
                .Genres.Where(g =>
                    g.NormalizedName.Contains(normalizedName) || g.NormalizedName == normalizedName
                )
                .OrderByDescending(g => g.Id)
                .Take(10)
                .ToListAsync(cancellationToken);

            return new SuccessApiResult<List<GenreResponse>>(
                genres.Select(g => g.ToResponse()).ToList()
            );
        }
    }
}
