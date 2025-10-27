namespace BibliotecaUteco.DataAccess.DbSetsActions;

public static class PenaltiesDbSetActions
{
    public static async Task<List<Penalty>> GetByFilterAsync(
        this DbSet<Penalty> set,
        bool? isDue = null,
        int? loanId = null,
        int skip = 0,
        int take = 10,
        CancellationToken cancellationToken = default
    )
    {
        var query = set.AsNoTracking().AsSplitQuery().AsQueryable();

        // Filtrar por estado de deuda
        if (isDue.HasValue)
        {
            query = query.Where(p => p.IsDue == isDue.Value);
        }

        // Filtrar por ID de préstamo
        if (loanId.HasValue && loanId > 0)
        {
            query = query.Where(p => p.LoanId == loanId.Value);
        }

        query = query.OrderByDescending(p => p.CreatedAt);

        return await query
            .Skip(skip)
            .Take(take)
            .Select(p => new Penalty
            {
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                LoanId = p.LoanId,
                OverdueDays = p.OverdueDays,
                IsDue = p.IsDue,
                DailyFineRate = p.DailyFineRate,
                TotalAmount = p.TotalAmount,
                ReturnedAmount = p.ReturnedAmount,
                GivenAmount = p.GivenAmount,
                TransactionId = p.TransactionId,
                ReaderId = p.Loan.ReaderId,
                ReaderName = p.Loan.Reader.FullName,
                ReaderIdentityCardNumber = p.Loan.Reader.IdentityCardNumber,
                ReaderStudentLicence = p.Loan.Reader.StudentLicence ,

            })
            .ToListAsync(cancellationToken);
    }

    public static async Task<Penalty?> GetByIdAsync(
        this DbSet<Penalty> set,
        int penaltyId,
        CancellationToken cancellationToken = default
    )
    {
        return await set.Where(p => p.Id == penaltyId)
            .Select(p => new Penalty
            {
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                LoanId = p.LoanId,
                OverdueDays = p.OverdueDays,
                IsDue = p.IsDue,
                DailyFineRate = p.DailyFineRate,
                TotalAmount = p.TotalAmount,
                ReturnedAmount = p.ReturnedAmount,
                GivenAmount = p.GivenAmount,
                TransactionId = p.TransactionId,
                ReaderId = p.Loan.ReaderId,
                ReaderName = p.Loan.Reader.FullName,
                ReaderIdentityCardNumber = p.Loan.Reader.IdentityCardNumber,
                ReaderStudentLicence = p.Loan.Reader.StudentLicence ,

            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
