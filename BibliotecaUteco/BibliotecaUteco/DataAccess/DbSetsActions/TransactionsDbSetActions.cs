namespace BibliotecaUteco.DataAccess.DbSetsActions;

public static class TransactionsDbSetActions
{
    public static async Task<List<Transaction>> GetByFilterAsync(
        this DbSet<Transaction> dbSet,
        string? userName = null,
        int skip = 0,
        int take = 10,
        CancellationToken token = default
    )
    {
        var query = dbSet.AsNoTracking().AsSplitQuery().AsQueryable();

        if (!string.IsNullOrEmpty(userName))
        {
            query = query.Where(x =>
                x.User.FullName.ToLower().Contains(userName.ToLower())
                || x.User.Username.ToLower().Contains(userName.ToLower())
            );
        }

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync(token);
    }
}
