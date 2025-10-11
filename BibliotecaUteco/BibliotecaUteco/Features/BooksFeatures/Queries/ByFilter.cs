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

namespace BibliotecaUteco.Features.BooksFeatures.Queries
{
    public class GetBooksByFilterCommand : ICommand<IApiResult>
    {
        [JsonPropertyName("bookName"), FromQuery(Name = "bookName"), Description("El nombre del libro a buscar")]
        public string? BookName { get; set; } = null;

        [JsonPropertyName("genreName"), FromQuery(Name = "genreName"), Description("El nombre del genero del libro a buscar")]

        public string? GenreName { get; set; } = null;

        [JsonPropertyName("authorName"), FromQuery(Name = "authorName"), Description("El nombre del autor del libro a buscar")]

        public string? AuthorName { get; set; } = null;

        [JsonPropertyName("take"), Range(1, 10), FromQuery(Name = "take"), Description("Cantidad de libros a tomar")]

        public int Take { get; set; } = 10;

        [JsonPropertyName("skip"), Range(0, int.MaxValue), FromQuery(Name = "skip"), Description("Cantidad de libros a omitir")]

        public int Skip { get; set; } = 0;
    }

    public class GetBooksByFilterCommandValidator : AbstractValidator<GetBooksByFilterCommand>
    {
        public GetBooksByFilterCommandValidator()
        {
            RuleFor(x => x.BookName)
                .MaximumLength(100).WithMessage("El nombre del libro no puede superar los 100 caracteres")
                .When(x => !string.IsNullOrEmpty(x.BookName));

            RuleFor(x => x.GenreName)
                .MaximumLength(50).WithMessage("El nombre del género no puede superar los 50 caracteres")
                .When(x => !string.IsNullOrEmpty(x.GenreName));

            RuleFor(x => x.AuthorName)
                .MaximumLength(100).WithMessage("El nombre del autor no puede superar los 100 caracteres")
                .When(x => !string.IsNullOrEmpty(x.AuthorName));

            RuleFor(x => x.Take)
                .InclusiveBetween(1, 10).WithMessage("La cantidad de libros a tomar debe estar entre 1 y 10");

            RuleFor(x => x.Skip)
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad de libros a omitir debe ser mayor o igual a 0");

           
        }
    }



internal class GetBooksByFilterEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(EndpointSettings.BooksEndpoint + "/by-filter", async (
                [AsParameters] GetBooksByFilterCommand command,
                ISender sender,
                IEndpointWrapper<GetBooksByFilterEndpoint> wrapper,
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
            .WithTags(nameof(Book))
            .WithName(nameof(GetBooksByFilterEndpoint))
            .WithDescription($"Retorna una lista de 10 libros según el filtro dado {nameof(IApiResult)}");
    }
}

public class GetGenresByNameHandler(IBibliotecaUtecoDbContext context) : ICommandHandler<GetBooksByFilterCommand, IApiResult>
    {
        public async Task<IApiResult> HandleAsync(GetBooksByFilterCommand request, CancellationToken cancellationToken = default)
        {
            var books = await context.Books.GetByFilterAsync(request.GenreName, request.BookName, request.AuthorName, request.Skip, request.Take);

            return ApiResult<List<BookResponse>>.BuildSuccess(books.Select(g => g.ToResponse()).ToList());
        }
    }
}