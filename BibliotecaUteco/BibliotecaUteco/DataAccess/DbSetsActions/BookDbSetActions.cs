using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaUteco.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUteco.DataAccess.DbSetsActions
{
    public static class BookDbSetActions
    {
        public static async Task<Book?> GetBookByIdAsync(this DbSet<Book> dbSet, int bookId)
        {

            return await dbSet.AsSplitQuery().AsNoTracking().Where(b => b.Id == bookId).Select(b => new Book()
            {
                Id = b.Id,
                Name = b.Name,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt,
                Sinopsis = b.Sinopsis,
                Authors = b.Authors,
                Genres = b.Genres,
                Stock = b.Stock,
                AvailableAmount = b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null)


            }).FirstOrDefaultAsync();
        }

        public static async Task<List<Book>> GetByFilterAsync(this DbSet<Book> dbSet, string? genreName = null, string? name = null, string? authorName = null, int skip = 0, int take = 10 )
        {

            var query = dbSet.AsSplitQuery().AsNoTracking().AsQueryable();

            if (genreName is not null)
            {
                var normalizedGenre = genreName.ToLower().Trim().Normalize();

                query.Where(b => b.Genres.Any(g => g.Genre.NormalizedName == normalizedGenre));
            }

            if (name is not null)
            {
                var normalizedName = name.ToLower().Trim().Normalize();
                query.Where(b => b.NormalizedName == normalizedName);

            }
            
            if(authorName is not null)
            {
                var normalizedAuthorName = authorName.ToLower().Trim().Normalize();
                query.Where(b => b.Authors.Any(a => a.Author.NormalizedFullName == normalizedAuthorName));

            }
                
            return await query.OrderBy(b => b.Id).Skip(skip).Take(take).Select(b => new Book()
            {
                Id = b.Id,
                Name = b.Name,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt,
                Sinopsis = b.Sinopsis,
                Authors = b.Authors,
                Genres = b.Genres,
                Stock = b.Stock,
                AvailableAmount = b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null)


            }).ToListAsync();
        }
    }
}