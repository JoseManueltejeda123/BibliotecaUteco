using BibliotecaUteco.Services;

namespace BibliotecaUteco.Features.BooksFeatures.Actions
{
    public class DeleteBookCommand : ICommand<IApiResult>
    {
        [FromQuery(Name = "bookId"), JsonPropertyName("bookId"), Description("Id del libro a eliminar"), Required]
        public int BookId { get; set; }
    }

    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("El ID del libro debe ser mayor a 0");
        }
    }

    internal class DeleteBookEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete(EndpointSettings.BooksEndpoint + "/delete", async (
                    [AsParameters] DeleteBookCommand command,
                    ISender sender,
                    IEndpointWrapper<DeleteBookEndpoint> wrapper,
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
                .Produces<IApiResult>(200, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(400, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(404, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(409, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(500, ApplicationContentTypes.ApplicationJson)
                .ProducesProblem(403, ApplicationContentTypes.ApplicationJson)
                .WithTags(nameof(Book))
                .WithName(nameof(DeleteBookEndpoint))
                .WithDescription("Elimina un libro por su ID. No se puede eliminar si tiene préstamos activos.");
        }
    }

    public class DeleteBookCommandHandler(
        IBibliotecaUtecoDbContext context,
        IFileUploadService fileUploadService) : ICommandHandler<DeleteBookCommand, IApiResult>
    {
        

        public async Task<IApiResult> HandleAsync(DeleteBookCommand request, CancellationToken cancellationToken = default)
        {
            var book = await context.Books
                .AsNoTracking()
                .AsSplitQuery()
                .IgnoreAutoIncludes()
                .FirstOrDefaultAsync(b => b.Id == request.BookId, cancellationToken);

            if (book == null)
            {
                return ApiResult<bool>.BuildFailure(HttpStatus.NotFound, "El libro no existe.");
            }

            if (await context.BookLoans.AnyAsync(b => b.BookId == request.BookId && b.Loan.ReturnedDate == null))
            {
                return ApiResult<bool>.BuildFailure(
                    HttpStatus.Conflict, 
                    "No se puede eliminar el libro porque tiene préstamos activos. Espere a que sean devueltos.");
            }

           var deletedRows = await context.Books
                .Where(b => b.Id == request.BookId)
                .ExecuteDeleteAsync(cancellationToken);

            if(deletedRows >= 1)
            {
                if (!string.IsNullOrEmpty(book.CoverUrl))
                {
                    fileUploadService.DeleteFile(book.CoverUrl, EnvFolders.BookCovers);
                }
            }

            return ApiResult<bool>.BuildSuccess(deletedRows >= 1);
        }
    }
}
