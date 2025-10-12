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

namespace BibliotecaUteco.Features.AuthorFeatures.Queries
{
    public class GetAuthorsByNameCommand : ICommand<IApiResult>
    {
        [FromQuery(Name = "authorsName"), JsonPropertyName("authorsName"), MaxLength(30, ErrorMessage = "El nombre del autor no puede superar los 30 caracteres"), Description("El nombre del autor a buscar")]
        public string? AuthorsName { get; set; }
    }


    public class GetAuthorsByNameCommandValidator : AbstractValidator<GetAuthorsByNameCommand>
    {
        public GetAuthorsByNameCommandValidator()
        {
            RuleFor(x => x.AuthorsName)
                .MaximumLength(30)
                .When(x => !string.IsNullOrEmpty(x.AuthorsName))
                .WithMessage("El nombre del autor no puede superar los 30 caracteres");
        }
    }

    
internal class GetAuthorsByNameEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(EndpointSettings.AuthorsEndpoint + "/get-by-name", async (
                [AsParameters] GetAuthorsByNameCommand command,
                ISender sender,
                IEndpointWrapper<GetAuthorsByNameEndpoint> wrapper,
                CancellationToken cancellationToken = default
            ) =>
            {
                return await wrapper.ExecuteAsync<IApiResult>(async () =>
                {

                    return await sender.SendAndValidateAsync(command, cancellationToken);
                }
                );
            })
            .RequireAuthorization(AuthorizationPolicies.AllowAuthorizedUsers)
            .DisableAntiforgery()
            .RequireCors()
            .Produces<ApiResult<JwtResponse>>(200, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(Author))
            .WithName(nameof(GetAuthorsByNameEndpoint))
            .WithDescription($"Retorna una lista de 5 autores segun un nombre dado {nameof(IApiResult)}");


    }
}


    public class AuthenticateUserCommandHandler(IBibliotecaUtecoDbContext context) : ICommandHandler<GetAuthorsByNameCommand, IApiResult>
    {
        public async Task<IApiResult> HandleAsync(GetAuthorsByNameCommand request, CancellationToken cancellationToken = default)
        {

            var authors = await context.Authors.GetAuthorsByName(request.AuthorsName ?? "");

            return ApiResult<List<AuthorResponse>>.BuildSuccess(authors.Select(a => a.ToResponse()).ToList());

        }
    }
}