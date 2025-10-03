using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.DataAccess.Context;
using BibliotecaUteco.DataAccess.Models;
using BibliotecaUteco.Helpers;
using BibliotecaUteco.Settings;
using BibliotecaUteco.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaUteco.Features.UserFeatures.Actions;
public class TestCommand : ICommand<IApiResult>
{
    public int Number { get; set; }
}

public class TestCommandValidator : AbstractValidator<AuthenticateUserCommand>{

    public TestCommandValidator()
    {
      

    }
}

internal class TestCommandEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(EndpointSettings.UsersEndpoint + "/test", async (
                [FromBody] TestCommand command,
                ISender sender,
                IEndpointWrapper<AuthenticateUserEndpoint> wrapper,
                CancellationToken cancellationToken = default
            ) =>
            {
                return await wrapper.ExecuteAsync<IApiResult>(async () =>
                {

                    return await sender.SendAndValidateAsync(command, cancellationToken);
                }
                );
            })
            .RequireAuthorization(AuthorizationPolicies.AllowAdminsOnly)
            .RequireCors(CorsPolicies.DefaultPolicy)
            .DisableAntiforgery()
            .Accepts<AuthenticateUserCommand>(false, ApplicationContentTypes.ApplicationJson)
            .Produces<ApiResult<JwtResponse>>(200, ApplicationContentTypes.ApplicationJson)
            .Produces<BadRequestApiResult>(400, ApplicationContentTypes.ApplicationJson)
            .Produces<NotFoundApiResult>(404, ApplicationContentTypes.ApplicationJson)
            .Produces<InternalServerErroApiResult>(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(User))
            .WithName(nameof(TestCommandEndpoint))
            .WithDescription($"Revisa las credenciales de un usuario y emite un {nameof(IApiResult)} con un JWT");


    }
}


public class TestCommandHandler(IBibliotecaUtecoDbContext context) : ICommandHandler<TestCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(TestCommand request, CancellationToken cancellationToken = default)
    {
        return new ApiResult<string>("Lol");

    }
}