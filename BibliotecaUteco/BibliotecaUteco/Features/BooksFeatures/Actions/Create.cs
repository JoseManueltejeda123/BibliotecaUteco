using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.DataAccess.Context;
using BibliotecaUteco.DataAccess.DbSetsActions;
using BibliotecaUteco.DataAccess.Models;
using BibliotecaUteco.Helpers;
using BibliotecaUteco.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUteco.Features.BooksFeatures.Actions
{
    public class CreateBookCommand : ICommand<IApiResult>
    {
        [MaxLength(50), MinLength(1), Required, FromForm(Name = "name"), JsonPropertyName("name"), Description("El nombre del libro a crear")]
        public string Name { get; set; } = null!;

        [Url, JsonPropertyName("coverUrl"), FromForm(Name = "coverUrl"), Description("Portada del libro")]
        public string? CoverUrl { get; set; }

        [MaxLength(500), MinLength(10), FromForm(Name = "sinopsis"), Required, Description("Breve sinopsis del libro"), JsonPropertyName("sinopsis")]
        public string Sinopsis { get; set; } = null!;

        [FromForm(Name = "authorIds"), JsonPropertyName("authorsIds"), Description("Id de los autores de este libro")]
        public List<int> AuthorIds { get; set; } = new();

        [FromForm(Name = "genreIds"), JsonPropertyName("genreIds"), Description("Id de los generos literarios de este libro")]
        public List<int> GenreIds { get; set; } = new();

        [JsonPropertyName("stock"), FromForm(Name = "stock"), MinLength(1), Required, Description("Cantidad de libros a disponer")]
        public int Stock { get; set; }

    }

    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del libro es obligatorio.")
                .MinimumLength(1).WithMessage("El nombre del libro debe tener al menos 1 carácter.")
                .MaximumLength(50).WithMessage("El nombre del libro no puede tener más de 50 caracteres.");

            RuleFor(x => x.Sinopsis)
                .NotEmpty().WithMessage("La sinopsis es obligatoria.")
                .MinimumLength(10).WithMessage("La sinopsis debe tener al menos 10 caracteres.")
                .MaximumLength(500).WithMessage("La sinopsis no puede tener más de 500 caracteres.");

            RuleFor(x => x.CoverUrl)
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("La URL de la portada no es válida.");

            RuleFor(x => x.Stock)
                .GreaterThan(0).WithMessage("El stock debe ser mayor que cero.");

            RuleFor(x => x.AuthorIds)
                .Must(ids => ids.All(id => id > 0))
                .WithMessage("Los IDs de autores deben ser válidos.");

            RuleFor(x => x.GenreIds)
                .NotEmpty().WithMessage("Debe especificar al menos un género literario.")
                .Must(ids => ids.All(id => id > 0))
                .WithMessage("Los IDs de géneros deben ser válidos.");
        }
    }

    internal class CreateBookEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost(EndpointSettings.BooksEndpoint, async (
                    [FromForm] CreateBookCommand command,
                    ISender sender,
                    IEndpointWrapper<CreateBookEndpoint> wrapper,
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
                .Accepts<CreateBookCommand>(false, ApplicationContentTypes.MultipartForm)
                .Produces<ApiResult<BookResponse>>(200, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(403, ApplicationContentTypes.ApplicationJson)
                .WithTags(nameof(Book))
                .WithName(nameof(CreateBookEndpoint))
                .WithDescription($"Crea un nuevo libro y retorna un {nameof(IApiResult)} con el libro creado");
        }
    }
    
    public class CreateBookCommandHandler(IBibliotecaUtecoDbContext context) 
    : ICommandHandler<CreateBookCommand, IApiResult>
    {
        public async Task<IApiResult> HandleAsync(CreateBookCommand request, CancellationToken cancellationToken = default)
        {
            var normalizedName = request.Name.ToLower().Trim().Normalize();

            if (await context.Books.AnyAsync(b => b.NormalizedName == normalizedName, cancellationToken))
            {
                return ApiResult<BookResponse>.BuildFailure(HttpStatus.Conflict, "Ya existe un libro con ese nombre.");
            }

            if(request.AuthorIds.Any())
            {
                if (!await context.Authors
                .AnyAsync(a => request.AuthorIds.Contains(a.Id)))
                {
                    return ApiResult<BookResponse>.BuildFailure(HttpStatus.BadRequest, "Uno o más autores no existen.");
                }
            
            }
           
            if(!await context.Genres
                .AnyAsync(a => request.GenreIds.Contains(a.Id)))
            {
                return ApiResult<BookResponse>.BuildFailure(HttpStatus.BadRequest, "Uno o más generos literarios no existen.");
            }

    

            await context.Books.AddAsync(Book.Create(request), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            context.ChangeTracker.Clear();

            var  books = await context.Books.GetByFilterAsync(name: normalizedName, take: 1);

            if (books.FirstOrDefault() is var book && book is null)
            {
                return ApiResult<BookResponse>.BuildFailure(HttpStatus.BadRequest, "El libro no pudo ser encontrado tras su creación.");
            }

            return ApiResult<BookResponse>.BuildSuccess(book.ToResponse());
        }
    }



}