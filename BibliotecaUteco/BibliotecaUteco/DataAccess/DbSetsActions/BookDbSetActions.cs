namespace BibliotecaUteco.DataAccess.DbSetsActions
{
    public static class BookDbSetActions
    {
        public static async Task<Book?> GetBookByIdAsync(this DbSet<Book> dbSet, int bookId, CancellationToken token = default)
        {

            return await dbSet.AsSplitQuery().AsNoTracking().Where(b => b.Id == bookId).Select(b => new Book()
            {
                Id = b.Id,
                Name = b.Name,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt,
                Synopsis = b.Synopsis,
                Authors = b.Authors,
                Genres = b.Genres,
                Stock = b.Stock,
                CoverUrl = b.CoverUrl,
                AvailableAmount = b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null),
                ActiveLoansCount = b.Loans.Count(l => l.Loan.ReturnedDate == null)


            }).FirstOrDefaultAsync(token);
        }

        

        public static async Task<List<Book>> GetByFilterAsync(this DbSet<Book> dbSet, string? genreName = null, string? name = null, string? authorName = null, int skip = 0, int take = 10, CancellationToken token = default )
        {

            var query = dbSet.AsSplitQuery().AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(genreName)) 
            {
                var normalizedGenre = genreName.NormalizeField();
                query = query.Where(b => b.Genres.Any(g => g.Genre.NormalizedName.Contains(normalizedGenre)));
            }

            if (!string.IsNullOrWhiteSpace(name)) 
            {
                var normalizedName = name.NormalizeField();
                query = query.Where(b => b.NormalizedName.Contains(normalizedName));
            }
            
            if (!string.IsNullOrWhiteSpace(authorName)) 
            {
                var normalizedAuthorName = authorName.NormalizeField();
                query = query.Where(b => b.Authors.Any(a => a.Author.NormalizedFullName.Contains(normalizedAuthorName) ));
            }
                        
            return await query.OrderByDescending(b => b.Id).Skip(skip).Take(take).Select(b => new Book()
            {
                Id = b.Id,
                Name = b.Name,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt,
                Synopsis = b.Synopsis,
                Authors = b.Authors,
                CoverUrl = b.CoverUrl,
                Genres = b.Genres,
                Stock = b.Stock,
                AvailableAmount = b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null),
                ActiveLoansCount = b.Loans.Count(l => l.Loan.ReturnedDate == null)



            }).ToListAsync(token);
        }
    }
}