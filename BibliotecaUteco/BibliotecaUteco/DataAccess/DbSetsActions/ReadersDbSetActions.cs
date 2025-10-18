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
            PhoneNumber = r.PhoneNumber,
            SexId = r.SexId,
            StudentLicence = r.StudentLicence,
            LastLoanDate = r.Loans.FirstOrDefault() != null ? r.Loans.FirstOrDefault()!.CreatedAt : null,
            LastLoanIsActive = r.Loans.Any(l => l.ReturnedDate == null),
            LoansCount = r.Loans.Count(),


        }).FirstOrDefaultAsync(cancellationToken);
    }
    
    public static async Task<List<Reader>> GetByFilterAsync(this DbSet<Reader> dbSet, string? identityCardNumber = null, string? studentLicence = null, int skip = 0, int take = 10, CancellationToken token = default )
    {

        var query = dbSet.AsSplitQuery().AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(identityCardNumber)) 
        {
            query = query.Where(u => u.IdentityCardNumber.Contains(identityCardNumber));
        }
        
        if (!string.IsNullOrWhiteSpace(studentLicence)) 
        {
            query = query.Where(u => u.StudentLicence != null && u.StudentLicence.Contains(studentLicence));
        }


        
                        
        return await query.OrderByDescending(b => b.Id).Skip(skip).Take(take).Select(r => new Reader()
        {
            Id = r.Id,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt,
            FullName = r.FullName,
            IdentityCardNumber = r.IdentityCardNumber,
            Address = r.Address,
            PhoneNumber = r.PhoneNumber,
            SexId = r.SexId,
            StudentLicence = r.StudentLicence,
            LastLoanDate = r.Loans.FirstOrDefault() != null ? r.Loans.FirstOrDefault()!.CreatedAt : null,
            LastLoanIsActive = r.Loans.Any(l => l.ReturnedDate == null),
            LoansCount = r.Loans.Count(),
            
        }).ToListAsync(token);
    }
}