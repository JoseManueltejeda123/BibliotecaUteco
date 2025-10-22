namespace BibliotecaUteco.DataAccess.DbSetsActions;

public static class TransactionsDbSetActions
{
    public static async Task<List<Transaction>> GetByFilterAsync(this DbSet<Transaction> dbSet, int? userId = null, int skip = 0, int take = 10, CancellationToken token =  default)
    {
        var query = dbSet.AsNoTracking().AsSplitQuery().AsQueryable();
        
        
        if(userId.HasValue && userId.Value > 0)
        {
            query = query.Where(x => x.UserId == userId.Value);
        }
        
        return await query.OrderByDescending(t => t.CreatedAt).Skip(skip).Take(take).ToListAsync(token);
    }
}