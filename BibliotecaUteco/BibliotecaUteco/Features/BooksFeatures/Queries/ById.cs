using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Features.BooksFeatures.Queries
{
    public class GetBookByIdCommand : ICommand<IApiResult>
    {
        [JsonPropertyName("bookId"), 
        FromQuery(Name = "bookId"), 
        Range(1, int.MaxValue), 
        Description("El id del libro a buscar")
        ]
        public int BookId { get; set; }
    }

    public class GetBookByIdCommandValidator : AbstractValidator<GetBookByIdCommand>
    {
        public GetBookByIdCommandValidator()
        {
            RuleFor(x => x.BookId).GreaterThanOrEqualTo(1).WithMessage("El id del libro debe de ser mayor o igual a 1");
        }
    }

    public class GetBookByIdEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet(EndpointSettings.BooksEndpoint + "/by-id", async (

                [AsParameters] GetBookByIdCommand command,
                ISender sender,
                IEndpointWrapper<GetBookByIdEndpoint> wrapper,
                CancellationToken token = default

            ) =>
            {

                return await wrapper.ExecuteAsync<IApiResult>(
                    async () =>
                    {
                        return await sender.SendAndValidateAsync(command, token);
                    }
                );
            })
            .RequireAuthorization(AuthorizationPolicies.AllowAuthorizedUsers)
            .RequireCors(CorsPolicies.DefaultPolicy)
            .DisableAntiforgery();


        }
    }

    internal class GetBookByIdCommandHandler(IBibliotecaUtecoDbContext context) : ICommandHandler<GetBookByIdCommand, IApiResult>
    {
        public async Task<IApiResult> HandleAsync(GetBookByIdCommand request, CancellationToken cancellationToken = default)
        {
            var book = await context.Books.GetBookByIdAsync(request.BookId);

            if (book is null)
            {
                return new NotFoundApiResult($"El libro con el id {request.BookId} no se encontró");
            }

            return new SuccessApiResult<BookResponse>(book.ToResponse());
        }
    }
}