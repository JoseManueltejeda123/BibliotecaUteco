using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaUteco.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUteco.DataAccess.DbSetsActions
{
    public static class AuthorsDbSetActions
    {
        public static async Task<List<Author>> GetAuthorsByName(this DbSet<Author> authorsDbSet, string authorName, int take = 5)
        {
            var normalized = authorName.Normalize().ToLower().Trim();
            return await authorsDbSet.AsNoTracking().AsSplitQuery().Where(a => a.NormalizedFullName.ToLower().Contains(normalized)).OrderBy(a => a.Id).Take(take).Select(a => new Author()
            {

                Id = a.Id,
                FullName = a.FullName,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt,
                BooksCount = a.Books.Count()

            }).ToListAsync();


        }
    }
}