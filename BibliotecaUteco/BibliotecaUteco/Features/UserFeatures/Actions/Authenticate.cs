using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.DataAccess.Context;
using BibliotecaUteco.DataAccess.Models;
using BibliotecaUteco.Helpers;
using BibliotecaUteco.Identity;
using BibliotecaUteco.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUteco.Features.UserFeatures.Actions;

public class AuthenticateUserCommand : ICommand<IApiResult>
{
    [Required, JsonPropertyName("username"), Description("El nombre de usuario del usuario a iniciar sesión"), FromBody, MinLength(5), MaxLength(15)]
    public required string Username { get; set; } 

    [Required, JsonPropertyName("password"), Description("La contraseña del usuario a iniciar sesión"), FromBody,
     MinLength(8), MaxLength(30)]
    public required string Password { get; set; } 
}

public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>{

    public AuthenticateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("El nombre de usuario es obligatorio")
            .MinimumLength(5).WithMessage("El nombre de usuario debe tener al menos 8 caracteres")
            .MaximumLength(15).WithMessage("El nombre de usuario no puede tener más de 15 caracteres");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria")
            .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
            .MaximumLength(30).WithMessage("La contraseña no puede tener más de 30 caracteres");
    }
}

internal class AuthenticateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(EndpointSettings.UsersEndpoint + "/authenticate", async (
                [FromBody] AuthenticateUserCommand command,
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
            .AllowAnonymous()
            .RequireCors(CorsPolicies.DefaultPolicy)
            .DisableAntiforgery()
            .Accepts<AuthenticateUserCommand>(false, ApplicationContentTypes.ApplicationJson)
            .Produces<ApiResult<JwtResponse>>(200, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
            .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
            .WithTags(nameof(User))
            .WithName(nameof(AuthenticateUserEndpoint))
            .WithDescription($"Revisa las credenciales de un usuario y emite un {nameof(IApiResult)} con un JWT");


    }
}


public class AuthenticateUserCommandHandler(IBibliotecaUtecoDbContext context, JwtBuilder jwtBuilder) : ICommandHandler<AuthenticateUserCommand, IApiResult>
{
    public async Task<IApiResult> HandleAsync(AuthenticateUserCommand request, CancellationToken cancellationToken = default)
    {
        var hashedPassword = request.Password.Hash();
        
        if (await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == hashedPassword)
                is var user && user is null)
        {
            return ApiResult<JwtResponse>.BuildFailure(HttpStatus.NotFound, "Credenciales incorrectas");
        }

        var token = jwtBuilder.GenerateToken(user.ToResponse());

        if (token is null)
        {
            return ApiResult<JwtResponse>.BuildFailure(HttpStatus.BadRequest,"Credenciales incorrectas");
            
        }

        return  ApiResult<JwtResponse>.BuildSuccess(token);

    }
}