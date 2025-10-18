using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

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
    public DbSet<BookLoan> BookLoans { get; set; }
    public DbSet<Reader>  Readers { get; set; }
    public DbSet<Penalty> Penalties { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Sex> Sexes { get; set; }
    ChangeTracker ChangeTracker { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DatabaseFacade Database { get; }


}