namespace BibliotecaUteco.DataAccess.DbSetsActions;

public static class GenreBookDbSetActions
{
    public static async Task SyncGenreBooksAsync(this DbSet<GenreBook> dbSet, int bookId, List<int> genreIds, CancellationToken cancellation = default)
    {
        var newGenres = genreIds.ToHashSet();
        
        await dbSet.Where(a => a.BookId == bookId && !newGenres.Contains(a.GenreId))
            .ExecuteDeleteAsync(cancellation);
         
        var currentGenres = await dbSet.AsNoTracking().IgnoreAutoIncludes().Where(b => b.BookId == bookId).Select(a => a.GenreId).ToListAsync(cancellation);

        var except = newGenres.Except(currentGenres);

        var genresToAdd = except.Select(a => new GenreBook() { BookId = bookId, GenreId = a });
         
        await dbSet.AddRangeAsync(genresToAdd, cancellation);


    }
}