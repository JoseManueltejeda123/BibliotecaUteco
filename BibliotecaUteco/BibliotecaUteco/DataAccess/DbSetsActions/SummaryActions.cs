using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.DataAccess.DbSetsActions
{
    public static class SummaryDbSetActions
    {
        public static async Task<ApplicationSummaryResponse> GetApplicationSummaryResponseAsync(this IBibliotecaUtecoDbContext context, DateTime? date = null, CancellationToken token = default)
        {
             if (!date.HasValue)
             {
                 date = DateTime.UtcNow;
             }
             
            var summary = await context.Books
             .Select(_ => new ApplicationSummaryResponse
             {
                 
                    
                BooksSummary = new BooksSummaryResponse
                {
                    AvailableBooks = context.Books.Where(x => x.CreatedAt <= date).Count(b => b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null && l.BookId == b.Id) >= 1),
                    LoanedBooks = context.Loans.Count(x => x.CreatedAt <= date),
                    NonAvailableBooks = context.Books.Where(x => x.CreatedAt <= date).Count(b => b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null && l.BookId == b.Id) <= 0),
                    TotalBooksCount = context.Books.Count(x => x.CreatedAt <= date)
                },

                LoansSummary = new LoanSummaryResponse
                {
                    ActiveLoans = context.Loans.Where(x => x.CreatedAt <= date).Count(l => l.ReturnedDate == null),
                    ReturnedLoans = context.Loans.Where(x => x.CreatedAt <= date).Count(l => l.ReturnedDate != null),
                    ExceededLoans = context.Loans.Where(x => x.CreatedAt <= date).Count(l => l.DueDate < date && l.ReturnedDate == null),
                    TotalLoansCount = context.Loans.Count(x => x.CreatedAt <= date),
                    NonReturnedLoans = context.Loans.Count(x => x.ReturnedDate == null),
                    
                }
             })
             .FirstAsync(token);

            return summary;
        }
        public static async Task<ApplicationSummaryResponse> GetApplicationSummaryResponsePreciseAsync(this IBibliotecaUtecoDbContext context, DateTime? date = null, CancellationToken token = default)
        {
            ApplicationSummaryResponse response = new();
            if (!date.HasValue)
            {
                date = DateTime.UtcNow;
            }
            else
            {
                date = date.Value.ToUniversalTime();   
            }

            var summary = await context.Books
                .Select(_ => new ApplicationSummaryResponse
                {
                 
                    
                    BooksSummary = new BooksSummaryResponse
                    {
                        AvailableBooks = context.Books.Where(x => x.CreatedAt == date).Count(b => b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null && l.BookId == b.Id) >= 1),
                        LoanedBooks = context.Loans.Count(x => x.CreatedAt == date),
                        NonAvailableBooks = context.Books.Where(x => x.CreatedAt == date).Count(b => b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null && l.BookId == b.Id) <= 0),
                        TotalBooksCount = context.Books.Count(x => x.CreatedAt == date)
                    },

                    LoansSummary = new LoanSummaryResponse
                    {
                        ActiveLoans = context.Loans.Where(x => x.CreatedAt == date).Count(l => l.ReturnedDate == null),
                        ReturnedLoans = context.Loans.Where(x => x.CreatedAt == date).Count(l => l.ReturnedDate != null),
                        ExceededLoans = context.Loans.Where(x => x.CreatedAt == date).Count(l => l.DueDate < DateTime.UtcNow && l.ReturnedDate == null),
                        TotalLoansCount = context.Loans.Count(x => x.CreatedAt == date)
                    }
                })
                .FirstAsync(token);

            return summary;
        }


        public static async Task<GeneralReport> GetGeneralReportForAMonthAsync(
            this IBibliotecaUtecoDbContext context, 
            int year, int month, 
            CancellationToken token = default)
        {
            var start = new DateTime(year, month, 1);
            var end   = start.AddMonths(1);

            var report = new GeneralReport()
            {
                Year = year,
                Month = month
            };

            //
            // 1. ESTADÍSTICAS
            //
            report.CurrentStateBooksCount = await context.Books.CountAsync(token);

            report.CurrentStateAvailableBooksCount =
                await context.Books.CountAsync(b =>
                    b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null) >= 1,
                    token);

            report.CurrentStateLoanedBooksCount =
                await context.Loans.Where(l => l.ReturnedDate == null)
                                .SumAsync(l => l.Books.Count(), token);

            //
            // READERS STATS
            //
            report.CurrentStateReadersCount = await context.Readers.CountAsync(token);

            report.CurrentStateUnactiveReaders =
                await context.Readers.CountAsync(r => r.Loans.All(l => l.ReturnedDate != null), token);

            report.CurrentStateActiveReaders =
                await context.Readers.CountAsync(r => r.Loans.Any(l => l.ReturnedDate == null), token);

            report.CurrentStateReadersWithExceededLoansCount =
                await context.Readers.CountAsync(
                    r => r.Loans.Any(l => l.DueDate < DateTime.UtcNow && l.ReturnedDate == null), 
                    token);


            //
            // LOANS STATS
            //
            report.CurrentSatetLoansCount = await context.Loans.CountAsync(token);

            report.CurrentStateExceededCount =
                await context.Loans.CountAsync(l => l.ReturnedDate == null && l.DueDate < DateTime.UtcNow, token);

            report.CurrentStateNotReturnedLoansCount =
                await context.Loans.CountAsync(l => l.ReturnedDate == null, token);

            report.CurrentStateReturnedLoansCount =
                await context.Loans.CountAsync(l => l.ReturnedDate != null, token);


            //
            // PENALTIES STATS
            //
            report.CurrentStateTotalPenalties = await context.Penalties.CountAsync(token);
            report.CurrentStatePayedPenalties = await context.Penalties.CountAsync(p => !p.IsDue, token);
            report.CurrentStateUnpayedPenalties = await context.Penalties.CountAsync(p => p.IsDue, token);


            //
            // CASHBOX
            //
            report.CurrentStateCashBox = await context.Transactions.SumAsync(t => t.Amount, token);
            report.CurrentStateTrasactionsCount = await context.Transactions.CountAsync(token);


            //
            // 2. LISTAS (separadas, limpias)
            //
            report.Books = await context.Books
                .Where(b => b.CreatedAt >= start && b.CreatedAt < end)
                .Include(b => b.Authors).ThenInclude(a => a.Author)
                .Include(b => b.Genres).ThenInclude(g => g.Genre)
                .Select(b => new BookResponse
                {
                    Id = b.Id,
                    Name = b.Name,
                    CreatedAt = b.CreatedAt,
                    Authors = b.Authors.Select(a => new BookAuthorResponse
                    {
                        Author = new AuthorResponse { FullName = a.Author.FullName }
                    }).ToList(),
                    Genres = b.Genres.Select(g => new GenreBookResponse
                    {
                        Genre = new GenreResponse { Name = g.Genre.Name }
                    }).ToList(),
                    LoansCount = b.Loans.Count(),
                    ActiveLoansCount = b.Loans.Count(l => l.Loan.ReturnedDate == null),
                    Stock = b.Stock,
                    AvailableAmount = b.AvailableAmount
                })
                .ToListAsync(token);


            report.Readers = await context.Readers
                .Where(r => r.CreatedAt >= start && r.CreatedAt < end)
                .Select(r => new ReaderResponse
                {
                    Id = r.Id,
                    FullName = r.FullName,
                    StudentLicence = r.StudentLicence ?? "",
                    IdentityCardNumber = r.IdentityCardNumber ?? "",
                    Passport = r.Passport ?? "",
                    LoansCount = r.Loans.Count()
                })
                .ToListAsync(token);


            report.Loans = await context.Loans
                .Where(l => l.CreatedAt >= start && l.CreatedAt < end)
                .Select(l => new LoanResponse
                {
                    Id = l.Id,
                    CreatedAt = l.CreatedAt,
                    ReturnedDate = l.ReturnedDate,
                    DueDate = l.DueDate,
                    BookCount = l.Books.Count(),
                    Reader = new ReaderResponse
                    {
                        FullName = l.Reader.FullName,
                        StudentLicence = l.Reader.StudentLicence ?? "",
                        IdentityCardNumber = l.Reader.IdentityCardNumber ?? "",
                        Passport = l.Reader.Passport ?? ""
                    }
                })
                .ToListAsync(token);


            report.Penalties = await context.Penalties
                .Where(p => p.CreatedAt >= start && p.CreatedAt < end)
                .Select(p => new PenaltyResponse
                {
                    Id = p.Id,
                    CreatedAt = p.CreatedAt,
                    IsDue = p.IsDue,
                    TransactionId = p.TransactionId,
                    DailyFineRate = p.DailyFineRate,
                    OverdueDays = p.OverdueDays,
                    TotalAmount = p.TotalAmount
                })
                .ToListAsync(token);


            report.Transactions = await context.Transactions
                .Where(t => t.CreatedAt >= start && t.CreatedAt < end)
                .Select(t => new TransactionResponse
                {
                    Id = t.Id,
                    CreatedAt = t.CreatedAt,
                    Amount = t.Amount,
                    User = new UserResponse { Username = t.User.Username }
                })
                .ToListAsync(token);

            return report;
        }


         public static async Task<GeneralReport> GetGeneralReportAsync(this IBibliotecaUtecoDbContext context, int year, CancellationToken token = default)
        {
            var start = new DateTime(year, 1, 1);
            var end = start.AddMonths(12);

             var report = new GeneralReport()
            {
                Year = year,
                Month = 0
            };

            //
            // 1. ESTADÍSTICAS
            //
            report.CurrentStateBooksCount = await context.Books.CountAsync(token);

            report.CurrentStateAvailableBooksCount =
                await context.Books.CountAsync(b =>
                    b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null) >= 1,
                    token);

            report.CurrentStateLoanedBooksCount =
                await context.Loans.Where(l => l.ReturnedDate == null)
                                .SumAsync(l => l.Books.Count(), token);

            //
            // READERS STATS
            //
            report.CurrentStateReadersCount = await context.Readers.CountAsync(token);

            report.CurrentStateUnactiveReaders =
                await context.Readers.CountAsync(r => r.Loans.All(l => l.ReturnedDate != null), token);

            report.CurrentStateActiveReaders =
                await context.Readers.CountAsync(r => r.Loans.Any(l => l.ReturnedDate == null), token);

            report.CurrentStateReadersWithExceededLoansCount =
                await context.Readers.CountAsync(
                    r => r.Loans.Any(l => l.DueDate < DateTime.UtcNow && l.ReturnedDate == null), 
                    token);


            //
            // LOANS STATS
            //
            report.CurrentSatetLoansCount = await context.Loans.CountAsync(token);

            report.CurrentStateExceededCount =
                await context.Loans.CountAsync(l => l.ReturnedDate == null && l.DueDate < DateTime.UtcNow, token);

            report.CurrentStateNotReturnedLoansCount =
                await context.Loans.CountAsync(l => l.ReturnedDate == null, token);

            report.CurrentStateReturnedLoansCount =
                await context.Loans.CountAsync(l => l.ReturnedDate != null, token);


            //
            // PENALTIES STATS
            //
            report.CurrentStateTotalPenalties = await context.Penalties.CountAsync(token);
            report.CurrentStatePayedPenalties = await context.Penalties.CountAsync(p => !p.IsDue, token);
            report.CurrentStateUnpayedPenalties = await context.Penalties.CountAsync(p => p.IsDue, token);


            //
            // CASHBOX
            //
            report.CurrentStateCashBox = await context.Transactions.SumAsync(t => t.Amount, token);
            report.CurrentStateTrasactionsCount = await context.Transactions.CountAsync(token);


            //
            // 2. LISTAS (separadas, limpias)
            //
            report.Books = await context.Books
                .Where(b => b.CreatedAt >= start && b.CreatedAt < end)
                .Include(b => b.Authors).ThenInclude(a => a.Author)
                .Include(b => b.Genres).ThenInclude(g => g.Genre)
                .Select(b => new BookResponse
                {
                    Id = b.Id,
                    Name = b.Name,
                    CreatedAt = b.CreatedAt,
                    Authors = b.Authors.Select(a => new BookAuthorResponse
                    {
                        Author = new AuthorResponse { FullName = a.Author.FullName }
                    }).ToList(),
                    Genres = b.Genres.Select(g => new GenreBookResponse
                    {
                        Genre = new GenreResponse { Name = g.Genre.Name }
                    }).ToList(),
                    LoansCount = b.Loans.Count(),
                    ActiveLoansCount = b.Loans.Count(l => l.Loan.ReturnedDate == null),
                    Stock = b.Stock,
                    AvailableAmount = b.AvailableAmount
                })
                .ToListAsync(token);


            report.Readers = await context.Readers
                .Where(r => r.CreatedAt >= start && r.CreatedAt < end)
                .Select(r => new ReaderResponse
                {
                    Id = r.Id,
                    FullName = r.FullName,
                    StudentLicence = r.StudentLicence ?? "",
                    IdentityCardNumber = r.IdentityCardNumber ?? "",
                    Passport = r.Passport ?? "",
                    LoansCount = r.Loans.Count()
                })
                .ToListAsync(token);


            report.Loans = await context.Loans
                .Where(l => l.CreatedAt >= start && l.CreatedAt < end)
                .Select(l => new LoanResponse
                {
                    Id = l.Id,
                    CreatedAt = l.CreatedAt,
                    ReturnedDate = l.ReturnedDate,
                    DueDate = l.DueDate,
                    BookCount = l.Books.Count(),
                    Reader = new ReaderResponse
                    {
                        FullName = l.Reader.FullName,
                        StudentLicence = l.Reader.StudentLicence ?? "",
                        IdentityCardNumber = l.Reader.IdentityCardNumber ?? "",
                        Passport = l.Reader.Passport ?? ""
                    }
                })
                .ToListAsync(token);


            report.Penalties = await context.Penalties
                .Where(p => p.CreatedAt >= start && p.CreatedAt < end)
                .Select(p => new PenaltyResponse
                {
                    Id = p.Id,
                    CreatedAt = p.CreatedAt,
                    IsDue = p.IsDue,
                    TransactionId = p.TransactionId,
                    DailyFineRate = p.DailyFineRate,
                    OverdueDays = p.OverdueDays,
                    TotalAmount = p.TotalAmount
                })
                .ToListAsync(token);


            report.Transactions = await context.Transactions
                .Where(t => t.CreatedAt >= start && t.CreatedAt < end)
                .Select(t => new TransactionResponse
                {
                    Id = t.Id,
                    CreatedAt = t.CreatedAt,
                    Amount = t.Amount,
                    User = new UserResponse { Username = t.User.Username }
                })
                .ToListAsync(token);

            return report;
           
        }
    }
}