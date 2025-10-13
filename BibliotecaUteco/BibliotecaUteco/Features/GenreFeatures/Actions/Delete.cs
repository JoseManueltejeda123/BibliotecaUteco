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

namespace BibliotecaUteco.Features.GenreFeatures.Actions
{
    public class DeleteGenreCommand : ICommand<IApiResult>
    {
        [Range(1, int.MaxValue)]
        [Required]
        [FromQuery(Name = "genreId")]
        [Description("El id del género a eliminar")]
        [JsonPropertyName("genreId")]
        public int GenreId { get; set; }
    }

    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(x => x.GenreId)
                .GreaterThan(0).WithMessage("El ID del género debe ser mayor a 0");
        }
    }

    internal class DeleteGenreEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete(EndpointSettings.GenresEndpoint + "/delete", async (
                    [AsParameters] DeleteGenreCommand command,
                    ISender sender,
                    IEndpointWrapper<DeleteGenreEndpoint> wrapper,
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
                .Accepts<DeleteGenreCommand>(false, ApplicationContentTypes.ApplicationJson)
                .Produces<IApiResult>(200, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(409, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(403, ApplicationContentTypes.ApplicationJson)
                .WithTags(nameof(Genre))
                .WithName(nameof(DeleteGenreEndpoint))
                .WithDescription("Elimina un género literario. No se puede eliminar si tiene libros asociados.");
        }
    }

    public class DeleteGenreCommandHandler(IBibliotecaUtecoDbContext _context) : ICommandHandler<DeleteGenreCommand, IApiResult>
    {
        

        public async Task<IApiResult> HandleAsync(DeleteGenreCommand request, CancellationToken cancellationToken = default)
        {
            
           var rows = await _context.Genres
                .Where(g => g.Id == request.GenreId)
                .ExecuteDeleteAsync(cancellationToken);

            if(rows <= 0)
            {
                return ApiResult<bool>.BuildFailure(HttpStatus.NotFound, "No se encontró el género");
            }
            return ApiResult<bool>.BuildSuccess(true);
        }
    }

}