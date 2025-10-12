using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.Settings;
using BibliotecaUteco.Client.Utilities;
using BibliotecaUteco.DataAccess.Context;
using BibliotecaUteco.DataAccess.DbSetsActions;
using BibliotecaUteco.DataAccess.Models;
using BibliotecaUteco.Helpers;
using BibliotecaUteco.Services;
using BibliotecaUteco.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUteco.Features.BooksFeatures.Actions;

public class UpdateBookCommand : ICommand<IApiResult>
{
    [FromForm(Name = "bookId"), JsonPropertyName("bookId"), Description("Id del libro a actualizar"), Required]
    public int BookId { get; set; } 
    
    [FromForm(Name = "bookName"), JsonPropertyName("bookName"), Description("El nuevo nombre del libro"), Required,
     MaxLength(50), MinLength(1)]
    public string BookName { get; set; } = "";
    
    [FromForm(Name="synopsis"), JsonPropertyName("synopsis"), Description("Nueva sinopsis"), Required, MinLength(10), MaxLength(500)]
    public string Synopsis { get; set; } = null!;
    
    [FromForm(Name = "stock"), JsonPropertyName("stock"), Description("Nuevo stocl"), Required, MinLength(10)]
    public int Stock { get; set; }

    [FromForm(Name = "genreIds"), JsonPropertyName("genreIds"), Description("Nuevos generos"), MinLength(1),
     MaxLength(5), Required]
    public List<int> GenreIds { get; set; } = new();
    
    [FromForm(Name = "authorIds"), JsonPropertyName("authorIds"), Description("Nuevos autores"),
     MaxLength(10), Required]
    public List<int> AuthorIds { get; set; } = new();
    
    [FromForm(Name = "coverFile"), JsonPropertyName("coverFil"), Description("Nueva portada")]
     public IFormFile? CoverFile { get; set; } = null;
}


public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("El ID del libro debe ser mayor a 0");

            RuleFor(x => x.BookName)
                .NotEmpty().WithMessage("El nombre del libro es requerido")
                .MinimumLength(1).WithMessage("El nombre debe tener al menos 1 carácter")
                .MaximumLength(50).WithMessage("El nombre no puede superar los 50 caracteres");

            RuleFor(x => x.Synopsis)
                .NotEmpty().WithMessage("La sinopsis es requerida")
                .MinimumLength(10).WithMessage("La sinopsis debe tener al menos 10 caracteres")
                .MaximumLength(500).WithMessage("La sinopsis no puede superar los 500 caracteres");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock debe ser mayor o igual a 0");

            RuleFor(x => x.GenreIds)
                .NotEmpty().WithMessage("Debe seleccionar al menos un género")
                .Must(list => list.Count >= 1 && list.Count <= 5)
                .WithMessage("Debe seleccionar entre 1 y 5 géneros")
                .Must(list => list.Distinct().Count() == list.Count)
                .WithMessage("No puede haber géneros duplicados");

            RuleFor(x => x.AuthorIds)
                .NotEmpty().WithMessage("Debe seleccionar al menos un autor")
                .Must(list => list.Count <= 10)
                .WithMessage("No puede seleccionar más de 10 autores")
                .Must(list => list.Distinct().Count() == list.Count)
                .WithMessage("No puede haber autores duplicados");

            When(x => x.CoverFile != null, () =>
            {
                RuleFor(x => x.CoverFile!.Length)
                    .Must(x => x <= FilesSettings.MaxFileSize)
                    .WithMessage("La portada no puede superar los 2MB");

                RuleFor(x => x.CoverFile!.ContentType)
                    .Must(x => FilesSettings.AllowedImageExtensionsForUpload.Contains(x))
                    .WithMessage("La portada debe ser una imagen (JPG, JPEG, PNG o WEBP)");
            });
        }
        
    }
    
     internal class UpdateBookEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut(EndpointSettings.BooksEndpoint, async (
                   [FromForm] UpdateBookCommand command,
                    ISender sender,
                    IEndpointWrapper<UpdateBookEndpoint> wrapper,
                    CancellationToken cancellationToken = default
                ) =>
                {
                    return await wrapper.ExecuteAsync<IApiResult>(async () => await sender.SendAndValidateAsync(command, cancellationToken));
                })
                .RequireAuthorization(AuthorizationPolicies.AllowAdminsOnly)
                .DisableAntiforgery()
                .Produces<ApiResult<BookResponse>>(200, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
                .WithTags(nameof(Book))
                .WithName(nameof(UpdateBookEndpoint))
                .WithDescription("Actualiza un libro existente incluyendo su portada, géneros y autores");
        }
    }


     public class UpdateBookCommandHandler(IBibliotecaUtecoDbContext context, IFileUploadService fileUploadService)
         : ICommandHandler<UpdateBookCommand, IApiResult>
     {
         public async Task<IApiResult> HandleAsync(UpdateBookCommand request,
             CancellationToken cancellationToken = default)
         {
             var normalizedName = request.BookName.NormalizeField();
             if (await context.Books.AnyAsync(b => b.NormalizedName == normalizedName && b.Id != request.BookId,
                     cancellationToken))
             {
                 return ApiResult<BookResponse>.BuildFailure(HttpStatus.Conflict, "Ya existe un libro diferente con este mismo nombre");
             }

             var bookToUpdate = await context.Books.IgnoreAutoIncludes().FirstOrDefaultAsync(b => b.Id == request.BookId, cancellationToken);
             
             if(bookToUpdate is null)
             {
                 return ApiResult<BookResponse>.BuildFailure(HttpStatus.NotFound,
                     "No pudimos encotrar el libro a actualizar");
             }

             await context.GenreBooks.SyncGenreBooksAsync(bookToUpdate.Id, request.GenreIds, cancellationToken);
             await context.BookAuthors.SyncBookAuthorsAsync(bookToUpdate.Id, request.AuthorIds, cancellationToken);
             bookToUpdate.Update(request);
             await context.SaveChangesAsync(cancellationToken);
             context.ChangeTracker.Clear();
             
             if(await context.Books.GetBookByIdAsync(request.BookId, cancellationToken) is var response && response is null)
             {
                 return ApiResult<BookResponse>.BuildFailure(HttpStatus.NotFound,
                     "No pudimos encotrar el libro despues de haberlo actualizado");
             }

             return ApiResult<BookResponse>.BuildSuccess(response.ToResponse());

         }
     }