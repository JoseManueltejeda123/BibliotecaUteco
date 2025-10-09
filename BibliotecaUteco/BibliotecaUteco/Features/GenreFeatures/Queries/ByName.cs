using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.DataAccess.Context;
using BibliotecaUteco.DataAccess.Models;
using BibliotecaUteco.Helpers;
using BibliotecaUteco.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            .MaximumLength(25).WithMessage("El nombre del género no puede tener más de 25 caracteres.");
    }
}

internal class GetGenresByNameEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(EndpointSettings.GenresEndpoint + "/by-name", async (
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
            })
            .RequireAuthorization(AuthorizationPolicies.AllowAuthorizedUsers)
            .RequireCors(CorsPolicies.DefaultPolicy)
            .DisableAntiforgery()
            .Produces<ApiResult<List<GenreResponse>>>(200, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Genre))
            .WithName(nameof(GetGenresByNameEndpoint))
            .WithDescription($"Retorna una lista de 5 géneros según el nombre dado {nameof(IApiResult)}");
    }
}

public class GetGenresByNameHandler(IBibliotecaUtecoDbContext context) : ICommandHandler<GetGenresByName, IApiResult>
{
    public async Task<IApiResult> HandleAsync(GetGenresByName request, CancellationToken cancellationToken = default)
    {
        var normalizedName = request.GenreName?.ToLower().Normalize().Trim() ?? "";
        var genres = await context.Genres.Where(g => g.NormalizedName.Contains(normalizedName)).OrderBy(g => g.Id).Take(5).ToListAsync(cancellationToken);

        return ApiResult<List<GenreResponse>>.BuildSuccess(genres.Select(g => g.ToResponse()).ToList());
    }
}
}