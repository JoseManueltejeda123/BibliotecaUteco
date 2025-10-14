using BibliotecaUteco.Client.Utilities;
using BibliotecaUteco.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUteco.DataAccess.DbSetsActions;

public static class UserDbSetActions
{
    public static async Task<List<User>> GetByFilterAsync(this DbSet<User> dbSet, string? userName = null,  CancellationToken token = default )
    {

        var query = dbSet.AsSplitQuery().AsNoTracking().AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(userName)) 
        {
            var normalizedUsername = userName.NormalizeField();
            query = query.Where(u => u.Username.Contains(normalizedUsername));
        }
                        
        return await query.OrderByDescending(b => b.Id).Select(b => new User()
        {
            Id = b.Id,
            FullName = b.FullName,
            CreatedAt = b.CreatedAt,
            UpdatedAt = b.UpdatedAt,
            Username = b.Username,
            IdentityCardNumber = b.IdentityCardNumber,
            ProfilePictureUrl = b.ProfilePictureUrl,
            Role = b.Role



        }).ToListAsync(token);
    }
}