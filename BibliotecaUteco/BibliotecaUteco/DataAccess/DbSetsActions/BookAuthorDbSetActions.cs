using BibliotecaUteco.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUteco.DataAccess.DbSetsActions;

public static class AuthorDbSetActions
{
    public static async Task SyncBookAuthorsAsync(this DbSet<BookAuthor> dbSet, int bookId, List<int> authorsIds, CancellationToken cancellation = default)
    {
        var newAuthors = authorsIds.ToHashSet();
        
         await dbSet.Where(a => a.BookId == bookId && !newAuthors.Contains(a.AuthorId))
            .ExecuteDeleteAsync(cancellation);
         
         var currentAuthors = await dbSet.AsNoTracking().IgnoreAutoIncludes().Where(b => b.BookId == bookId).Select(a => a.AuthorId).ToListAsync(cancellation);

         var except = newAuthors.Except(currentAuthors);

         var authorsToAdd = except.Select(a => new BookAuthor() { BookId = bookId, AuthorId = a });
         
         await dbSet.AddRangeAsync(authorsToAdd, cancellation);


    }
}