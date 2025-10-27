namespace BibliotecaUteco.DataAccess.DbSetsActions;

public static class LoanDbSetActions
{
    public static async Task<Loan?> GetByIdAsync(
        this DbSet<Loan> set,
        int loanId,
        CancellationToken cancellationToken = default
    )
    {
        return await set.Where(l => l.Id == loanId)
            .Select(l => new Loan
            {
                Id = l.Id,
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt,
                MaxLoanDays = l.MaxLoanDays,
                DueDate = l.DueDate,
                ReturnedDate = l.ReturnedDate,
                LoanedBooks = l
                    .Books.Select(b => b.Book)
                    .Select(b => new Book()
                    {
                        Id = b.Id,
                        CreatedAt = b.CreatedAt,
                        Name = b.Name,
                        CoverUrl = b.CoverUrl,
                        Authors = b
                            .Authors.Select(ba => new BookAuthor() { Author = ba.Author })
                            .ToList(),
                    })
                    .ToList(),

                Reader = new Reader()
                {
                    Id = l.Reader.Id,
                    FullName = l.Reader.FullName,
                    IdentityCardNumber = l.Reader.IdentityCardNumber,
                    StudentLicence = l.Reader.StudentLicence,
                },
                ReaderId = l.ReaderId,
                HasPenalty = l.Penalty != null,
                BookCount = l.Books.Count(),
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public static async Task<List<Loan>> GetByFilter(
        this DbSet<Loan> set,
        string? identityCardNumber = null,
        string? studentLicence = null,
        bool? justPendingOnes = null,
        bool? justReturnedOnes = null,
        bool? justExceededOnes = null,
        int skip = 0,
        int take = 5,
        CancellationToken cancellationToken = default
    )
    {
        var query = set.AsNoTracking().AsSplitQuery().AsQueryable();

        if (!string.IsNullOrWhiteSpace(identityCardNumber))
        {
            query = query.Where(l =>
                l.Reader != null
                && !string.IsNullOrEmpty(l.Reader.IdentityCardNumber)
                && l.Reader.IdentityCardNumber.Contains(identityCardNumber)
            );
        }

        // Filtrar por matrícula estudiantil
        if (!string.IsNullOrWhiteSpace(studentLicence))
        {
            query = query.Where(l =>
                l.Reader != null
                && !string.IsNullOrEmpty(l.Reader.StudentLicence)
                && l.Reader.StudentLicence.Contains(studentLicence)
            );
        }

        if (justPendingOnes.HasValue && justPendingOnes.Value)
        {
            query = query.Where(l => l.ReturnedDate == null);
        }
        else if (justExceededOnes.HasValue && justExceededOnes.Value)
        {
            query = query.Where(l => l.ReturnedDate == null && l.DueDate < DateTime.UtcNow);
        }
        else if (justReturnedOnes.HasValue && justReturnedOnes.Value)
        {
            query = query.Where(l => l.ReturnedDate != null);
        }

        query = query.OrderByDescending(l => l.Id);

        return await query
            .Skip(skip)
            .Take(take)
            .Select(l => new Loan
            {
                Id = l.Id,
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt,
                MaxLoanDays = l.MaxLoanDays,
                DueDate = l.DueDate,
                ReturnedDate = l.ReturnedDate,
                LoanedBooks = l
                    .Books.Select(b => b.Book)
                    .Select(b => new Book()
                    {
                        Id = b.Id,
                        CreatedAt = b.CreatedAt,
                        Name = b.Name,
                        CoverUrl = b.CoverUrl,
                        Authors = b
                            .Authors.Select(ba => new BookAuthor() { Author = ba.Author })
                            .ToList(),
                    })
                    .ToList(),

                Reader = new Reader()
                {
                    Id = l.Reader.Id,
                    FullName = l.Reader.FullName,
                    IdentityCardNumber = l.Reader.IdentityCardNumber,
                    StudentLicence = l.Reader.StudentLicence,
                },
                ReaderId = l.ReaderId,
                HasPenalty = l.Penalty != null,
                BookCount = l.Books.Count(),
            })
            .ToListAsync(cancellationToken);
    }
}
