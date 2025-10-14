using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.Utilities;
using BibliotecaUteco.DataAccess.Context;
using BibliotecaUteco.DataAccess.DbSetsActions;
using BibliotecaUteco.DataAccess.Models;
using BibliotecaUteco.Helpers;
using BibliotecaUteco.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUteco.Features.UserFeatures.Queries;

    public class GetUserByNameQuery : ICommand<IApiResult>
    {
        [FromQuery(Name = "username"), JsonPropertyName("username"), MaxLength(15)]
        [Description("Nombre de usuario a buscar")]
        public string? Username { get; set; } 
    }

    public class GetUserByNameQueryValidator : AbstractValidator<GetUserByNameQuery>
    {
        public GetUserByNameQueryValidator()
        {
            RuleFor(x => x.Username)
                .Must(x => string.IsNullOrEmpty(x) || x.Length <= 15)
               .WithMessage("El nombre de usuario no puede superar los 15 caracteres");
        }
    }

    internal class GetUserByNameEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet(EndpointSettings.UsersEndpoint + "/by-filter", async (
                    [AsParameters] GetUserByNameQuery query,
                    ISender sender,
                    IEndpointWrapper<GetUserByNameEndpoint> wrapper,
                    CancellationToken cancellationToken = default
                ) =>
                {
                    return await wrapper.ExecuteAsync<IApiResult>(async () =>
                    {
                        return await sender.SendAndValidateAsync(query, cancellationToken);
                    });
                })
                .RequireAuthorization(AuthorizationPolicies.AllowAuthorizedUsers)
                .RequireCors(CorsPolicies.DefaultPolicy)
                .DisableAntiforgery()
                .Produces<ApiResult<UserResponse>>(200, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
                .WithTags(nameof(User))
                .WithName(nameof(GetUserByNameEndpoint))
                .WithDescription("Busca una lista de usuarios por su nombre de usuario");
        }
    }

    public class GetUserByNameQueryHandler(IBibliotecaUtecoDbContext context)
        : ICommandHandler<GetUserByNameQuery, IApiResult>
    {
        public async Task<IApiResult> HandleAsync(GetUserByNameQuery request, CancellationToken cancellationToken = default)
        {
            var normalizedUsername = request.Username?.NormalizeField();

            var users = await context.Users.GetByFilterAsync(request.Username, cancellationToken);

            return ApiResult<List<UserResponse>>.BuildSuccess(users.Select(u => u.ToResponse()).ToList());
        }
    }
