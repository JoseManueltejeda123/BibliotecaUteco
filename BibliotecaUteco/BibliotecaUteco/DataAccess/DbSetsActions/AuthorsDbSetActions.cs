namespace BibliotecaUteco.DataAccess.DbSetsActions
{
    public static class AuthorsDbSetActions
    {
        public static async Task<List<Author>> GetAuthorsByName(this DbSet<Author> authorsDbSet, string authorName, int take = 5, CancellationToken token = default)
        {
            var normalized = authorName.NormalizeField();
            return await authorsDbSet.AsNoTracking().AsSplitQuery().Where(a => a.NormalizedFullName.ToLower().Contains(normalized)).OrderBy(a => a.Id).Take(take).Select(a => new Author()
            {

                Id = a.Id,
                FullName = a.FullName,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt,
                BooksCount = a.Books.Count()

            }).ToListAsync(token);


        }
    }
}