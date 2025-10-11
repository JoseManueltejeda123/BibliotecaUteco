using BibliotecaUteco.DataAccess.Models;
using BibliotecaUteco.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUteco.DataAccess.Context;

public class BibliotecaUtecoDbContext(DbContextOptions<BibliotecaUtecoDbContext> options) : DbContext(options), IBibliotecaUtecoDbContext
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Role>(x =>
        {
            x.HasMany(r => r.Users).WithOne(u => u.Role).HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.Restrict);
            x.HasIndex(u => u.Name).IsUnique(true);
            x.HasData(new Role[]
            {
                new Role()
                {
                    Id = 1,
                    Name = "Librarian",

                },
                new Role()
                {
                    Id = 2,
                    Name = "Admin",

                }

            });
        });
        
        modelBuilder.Entity<User>(x =>
        {
            x.HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId).IsRequired(true);
            x.HasIndex(u => u.Username).IsUnique(true);
            x.HasIndex(u => u.IdentityCardNumber).IsUnique(true);
            x.HasMany(u => u.Transactions).WithOne(t => t.User).HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            x.Navigation(u => u.Role).AutoInclude(true).IsRequired(true);
            x.HasData(new User[]
            {
                new User()
                {
                    Id = 1,
                    FullName = "José Apolinar",
                    Username = "jose.apolinar",
                    Password = new string("Jose.Apolinar1").Hash(),
                    IdentityCardNumber = "00112345678",
                    RoleId = 1
                },
                new User()
                {
                    Id = 2,
                    FullName = "Manuel López",
                    Username = "manuel.lopez",
                    Password = new string("Manuel.Lopez1").Hash(),
                    IdentityCardNumber = "00212345678",
                    RoleId = 2
                }

            });
        });
        
        modelBuilder.Entity<Author>(x =>
        {
            x.HasMany(u => u.Books).WithOne(r => r.Author).HasForeignKey(u => u.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            x.HasIndex(a => a.NormalizedFullName).IsUnique(true);

            
        });
        
      
        
        modelBuilder.Entity<Book>(x =>
        {
            x.HasMany(b => b.Authors).WithOne(ba => ba.Book).HasForeignKey(ba => ba.BookId).OnDelete(DeleteBehavior.Cascade);
            x.HasMany(b => b.Genres).WithOne(gb => gb.Book).HasForeignKey(u => u.BookId).OnDelete(DeleteBehavior.Cascade);
            x.HasMany(b => b.Loans).WithOne(l => l.Book).HasForeignKey(l => l.BookId).OnDelete(DeleteBehavior.Cascade);
            x.Navigation(b => b.Authors).AutoInclude(true);
            x.Navigation(b => b.Genres).AutoInclude(true);
            x.HasIndex(b => b.NormalizedName).IsUnique(true);

        });
        
        modelBuilder.Entity<BookAuthor>(x =>
        {
            x.HasOne(ba => ba.Book).WithMany(ba => ba.Authors).HasForeignKey(ba => ba.BookId).IsRequired(true);
            x.HasOne(ba => ba.Author).WithMany(ba => ba.Books).HasForeignKey(ba => ba.AuthorId).IsRequired(true);
            x.HasIndex(ba => new { ba.AuthorId, ba.BookId }).IsUnique(true);
            x.Navigation(ba => ba.Author).AutoInclude(true).IsRequired(true);


        });
    
        modelBuilder.Entity<Genre>(x =>
        {
            x.HasMany(g => g.Books).WithOne(gb => gb.Genre).HasForeignKey(ba => ba.GenreId).OnDelete(DeleteBehavior.Cascade);
            x.HasData(new[]
            {
                new Genre()
                {
                    Id = 1,
                    Name = "Fantasía",
                    NormalizedName = "fantasia"
                },
                new Genre()
                {
                    Id = 2,
                    Name = "Terror",
                    NormalizedName = "terror"
                },
                new Genre()
                {
                    Id = 3,
                    Name = "Ciencia Ficción",
                    NormalizedName = "ciencia ficcion"

                },
                new Genre()
                {
                    Id = 4,
                    Name = "Romance",
                    NormalizedName = "romance"

                },
                new Genre()
                {
                    Id = 5,
                    Name = "Misterio",
                    NormalizedName = "misterio"

                },
                new Genre()
                {
                    Id = 6,
                    Name = "Aventura",
                    NormalizedName = "aventura"

                },
                new Genre()
                {
                    Id = 7,
                    Name = "Histórico",
                    NormalizedName = "historico"

                },
                new Genre()
                {
                    Id = 8,
                    Name = "Biografía",
                    NormalizedName = "biografia"

                },
                new Genre()
                {
                    Id = 9,
                    Name = "Poesía",
                    NormalizedName = "poesia"

                },
                new Genre()
                {
                    Id = 10,
                    Name = "Drama",
                    NormalizedName = "drama"

                }
            });
            x.HasIndex(g => g.NormalizedName).IsUnique(true);
        });
        
        modelBuilder.Entity<GenreBook>(x =>
        {
            x.HasOne(gb => gb.Book).WithMany(gb => gb.Genres).HasForeignKey(ba => ba.BookId).IsRequired(true);
            x.HasOne(gb => gb.Genre).WithMany(ba => ba.Books).HasForeignKey(ba => ba.GenreId).IsRequired(true);
            x.HasIndex(ba => new { ba.GenreId, ba.BookId }).IsUnique(true);
            x.Navigation(ba => ba.Genre).AutoInclude(true).IsRequired(true);


        });
        
        modelBuilder.Entity<BookLoan>(x =>
        {
            x.HasOne(gb => gb.Book).WithMany(gb => gb.Loans).HasForeignKey(ba => ba.BookId).IsRequired(true);
            x.HasOne(gb => gb.Loan).WithMany(ba => ba.Books).HasForeignKey(ba => ba.LoanId).IsRequired(true);
            x.HasIndex(ba => new { ba.LoanId, ba.BookId }).IsUnique(true);
            x.Navigation(ba => ba.Loan).AutoInclude(true).IsRequired(true);


        });

        modelBuilder.Entity<Reader>(x =>
        {

            x.HasMany(u => u.Loans).WithOne(l => l.Reader).HasForeignKey(l => l.ReaderId)
                .OnDelete(DeleteBehavior.Restrict);
            x.HasIndex(u => u.IdentityCardNumber).IsUnique(true);
            x.HasIndex(u => u.StudentLicence).IsUnique(true);
        });
        
        
        modelBuilder.Entity<Penalty>(x =>
        {
            x.Navigation(p => p.Loan).AutoInclude(true).IsRequired(true);
        });
        
        modelBuilder.Entity<Loan>(x =>
        {

            x.HasOne(l => l.Penalty).WithOne(l => l.Loan).HasForeignKey<Penalty>(l => l.LoanId)
                .OnDelete(DeleteBehavior.Restrict);
            x.HasMany(l => l.Books).WithOne(l => l.Loan).HasForeignKey(l => l.LoanId)
                .OnDelete(DeleteBehavior.Cascade);
            
        });
        modelBuilder.Entity<Transaction>(x =>
        {

            x.HasOne(t => t.Penalty).WithOne(p => p.Transaction).HasForeignKey<Penalty>(l => l.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);
            x.HasOne(t => t.User).WithMany(u => u.Transactions).HasForeignKey(u => u.UserId).IsRequired(true);
            x.Navigation(t => t.User).IsRequired(true).AutoInclude(true);


        });





    }
}