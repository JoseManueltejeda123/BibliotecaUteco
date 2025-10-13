using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.Utilities;
using BibliotecaUteco.DataAccess.Context;
using BibliotecaUteco.DataAccess.Models;
using BibliotecaUteco.Helpers;
using BibliotecaUteco.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUteco.Features.GenreFeatures.Actions
{
   
    public class UpdateGenreCommand : ICommand<IApiResult>
    {
        [MaxLength(25)]
        [MinLength(1)]
        [Required]
        [FromBody]
        [Description("El nombre del genero a actualizar")]
        [JsonPropertyName("genreName")]
        public string GenreName { get; set; } = null!;

        [Range(1,int.MaxValue)]
        [Required]
        [FromBody]
        [Description("El id del genero a actualizar")]
        [JsonPropertyName("genreId")]
        public int GenreId { get; set; } 
    }
    
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.GenreName)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(1).WithMessage("El nombre del genero literario debe tener al menos 1 carácter.")
                .MaximumLength(25).WithMessage("El nombre del genero literario no puede tener más de 25 caracteres.");
        }
    }
    
    internal class UpdateGenreEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut(EndpointSettings.GenresEndpoint, async (
                    [FromBody] UpdateGenreCommand command,
                    ISender sender,
                    IEndpointWrapper<UpdateGenreEndpoint> wrapper,
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
                .WithName(nameof(UpdateGenreEndpoint))
                .WithDescription($"Actualiza un nuevo género literario y retorna un {nameof(IApiResult)} con el género creado");
        }
    }
    
    public class UpdateGenreCommandHandler(IBibliotecaUtecoDbContext context) : ICommandHandler<UpdateGenreCommand, IApiResult>
    {
        public async Task<IApiResult> HandleAsync(UpdateGenreCommand request, CancellationToken cancellationToken = default)
        {
            var normalizedName = request.GenreName.NormalizeField();


            if (await context.Genres.AnyAsync(g => g.NormalizedName == normalizedName && g.Id != request.GenreId, cancellationToken))
            {
                return ApiResult<GenreResponse>.BuildFailure(HttpStatus.Conflict, "Ya existe un género con ese nombre.");
            }
            
            var genre = await context.Genres.FirstOrDefaultAsync(g => g.Id == request.GenreId, cancellationToken);

            if(genre is null)
            {
                return ApiResult<GenreResponse>.BuildFailure(HttpStatus.NotFound, "No se encontro el genero literario");
            }


            genre.Update(request);
            await context.SaveChangesAsync(cancellationToken);
            context.ChangeTracker.Clear();

           
    
            return ApiResult<GenreResponse>.BuildSuccess(genre.ToResponse());
        }
    }
}