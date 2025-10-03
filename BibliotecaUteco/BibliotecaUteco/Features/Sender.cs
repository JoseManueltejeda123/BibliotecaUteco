using System.ComponentModel.DataAnnotations;
using BibliotecaUteco.DataAccess.Models;
using BibliotecaUteco.Helpers;
using FluentValidation;

namespace BibliotecaUteco.Features;

public class Sender(IServiceProvider serviceProvider) : ISender
{
    public async Task<TResponse> SendAndValidateAsync<TResponse>(
        ICommand<TResponse> command,
        CancellationToken cancellationToken = default
    )
    {
        
        var commandType = command.GetType();
        var validatorType = typeof(IValidator<>).MakeGenericType(commandType);
        var validator = serviceProvider.GetService(validatorType);
        if (validator is not null)
        {
            var result = await ((dynamic)validator).ValidateAsync(
                (dynamic)command,
                cancellationToken
            );
            ValidatorHelper.ValidateRequest(result);
        }
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(
            command.GetType(),
            typeof(TResponse)
        );
        dynamic handler = serviceProvider.GetRequiredService(handlerType);

        return await handler.HandleAsync((dynamic)command, cancellationToken);
    }
}