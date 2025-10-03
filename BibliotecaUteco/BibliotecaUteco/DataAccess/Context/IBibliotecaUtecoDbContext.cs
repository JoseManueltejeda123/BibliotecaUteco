using BibliotecaUteco.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BibliotecaUteco.DataAccess.Context;

public interface IBibliotecaUtecoDbContext
{
    public DbSet<User> Users { get; set; } 
    public DbSet<Role> Roles { get; set; }
    public DbSet<Book>  Books { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<GenreBook> GenreBooks { get; set; }
    ChangeTracker ChangeTracker { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


}