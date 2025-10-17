namespace BibliotecaUteco.DataAccess.DbSetsActions;

public static class ReadersDbSetActions
{
    public static async Task<Reader?> GetByIdAsync(this DbSet<Reader> dbSet, int readerId,
        CancellationToken cancellationToken = default)

    {
        return await dbSet.AsSingleQuery().AsNoTracking().Where(r => r.Id == readerId).Select(r => new Reader()
        {
            Id = r.Id,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt,
            FullName = r.FullName,
            IdentityCardNumber = r.IdentityCardNumber,
            Address = r.Address,
            StudentLicence = r.StudentLicence,
            LastLoanDate = r.Loans.FirstOrDefault() != null ? r.Loans.FirstOrDefault()!.CreatedAt : null,
            LastLoanIsActive = r.Loans.Any(l => l.ReturnedDate == null),
            LoansCount = r.Loans.Count(),


        }).FirstOrDefaultAsync(cancellationToken);
    }
}