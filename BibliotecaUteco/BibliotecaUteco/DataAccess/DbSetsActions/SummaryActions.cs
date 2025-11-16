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

        public static async Task<GeneralReport> GetGeneralReportForAMonthAsync(this IBibliotecaUtecoDbContext context, int year, int month, CancellationToken token = default)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);
            return await context.Books.Select(_ => new GeneralReport(){

                CurrentStateAvailableBooksCount = context.Books.Count(b => b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null && l.BookId == b.Id) >= 1),
                CurrentStateBooksCount = context.Books.Count(),
                CurrentStateLoanedBooksCount = context.Loans.Where(l => l.ReturnedDate == null).Sum(l => l.Books.Count()),
                Year = year,
                Month = month,
                

                Books = context.Books.Where(b => b.CreatedAt >= startDate && b.CreatedAt < endDate).Select(b => new BookResponse()
                {
                    Id = b.Id,
                    Name = b.Name,
                    CreatedAt = b.CreatedAt,
                    Authors = b.Authors.Select(a => new BookAuthorResponse(){
                        Author = new()
                        {
                            FullName = a.Author.FullName
                        }
                    }).ToList(),
                    Genres = b.Genres.Select(g => new GenreBookResponse()
                    {
                        Genre = new(){
                            Name = g.Genre.Name
                        }
                    }).ToList(),
                    LoansCount = b.Loans.Count(),
                    ActiveLoansCount = b.Loans.Count(),
                    Stock = b.Stock,
                    AvailableAmount = b.AvailableAmount
                }).ToList(),

                CurrentStateUnactiveReaders = context.Readers.Count(l => l.Loans.All(a => a.ReturnedDate != null)),
                CurrentStateActiveReaders = context.Readers.Count(l => l.Loans.Any(r => r.ReturnedDate == null)),
                CurrentStateReadersCount = context.Readers.Count(),
                CurrentStateReadersWithExceededLoansCount = context.Readers.Count(r => r.Loans.Any(l => l.DueDate < DateTime.UtcNow && l.ReturnedDate == null)),
                Readers = context.Readers.Where(b => b.CreatedAt >= startDate && b.CreatedAt < endDate).Select(l => new ReaderResponse()
                {
                    Id = l.Id,
                    FullName = l.FullName,
                    StudentLicence = l.StudentLicence ?? "",
                    IdentityCardNumber = l.IdentityCardNumber ?? "",
                    Passport = l.Passport ?? "",
                    LoansCount = l.Loans.Count()

                }).ToList(),

               
                CurrentSatetLoansCount = context.Loans.Count(),
                CurrentStateExceededCount = context.Loans.Count(l => l.ReturnedDate == null && l.DueDate < DateTime.UtcNow),
                CurrentStateNotReturnedLoansCount = context.Loans.Count(r => r.ReturnedDate == null),
                CurrentStateReturnedLoansCount = context.Loans.Count(r => r.ReturnedDate != null),
                Loans = context.Loans.Where(b => b.CreatedAt >= startDate && b.CreatedAt < endDate).Select(l => new LoanResponse()
                {
                    Id = l.Id,
                    CreatedAt = l.CreatedAt,
                    ReturnedDate = l.ReturnedDate,
                    DueDate = l.DueDate,
                    BookCount = l.Books.Count(),
                    Reader = new ReaderResponse(){

                        FullName = l.Reader.FullName,
                        StudentLicence = l.Reader.StudentLicence ?? "",
                        IdentityCardNumber = l.Reader.IdentityCardNumber ?? "",
                        Passport = l.Reader.Passport ?? ""
                    }


                    

                }).ToList(),

                CurrentStatePayedPenalties = context.Penalties.Count(p => !p.IsDue),
                CurrentStateUnpayedPenalties = context.Penalties.Count(p => p.IsDue),
                CurrentStateTotalPenalties = context.Penalties.Count(),
                Penalties = context.Penalties.Where(b => b.CreatedAt >= startDate && b.CreatedAt < endDate).Select(l => new PenaltyResponse()
                {
                    Id = l.Id,
                    CreatedAt = l.CreatedAt,
                    IsDue = l.IsDue,
                    TransactionId = l.TransactionId,
                    DailyFineRate = l.DailyFineRate,
                    OverdueDays = l.OverdueDays,
                    TotalAmount = l.TotalAmount

                   


                    

                }).ToList(),

                CurrentStateCashBox = context.Transactions.Sum(t => t.Amount),
                CurrentStateTrasactionsCount = context.Transactions.Count(),
                Transactions = context.Transactions.Where(b => b.CreatedAt >= startDate && b.CreatedAt < endDate).Select(l => new TransactionResponse()
                {
                    Id = l.Id,
                    CreatedAt = l.CreatedAt,
                    Amount = l.Amount,
                    User = new()
                    {
                        Username = l.User.Username
                    }

                   


                    

                }).ToList(),
                
                
               
            }).FirstOrDefaultAsync() ?? new();
        }

         public static async Task<GeneralReport> GetGeneralReportAsync(this IBibliotecaUtecoDbContext context, int year, CancellationToken token = default)
        {
            var startDate = new DateTime(year, 1, 1);
            var endDate = startDate.AddMonths(12);
            return await context.Books.Select(_ => new GeneralReport(){

                CurrentStateAvailableBooksCount = context.Books.Count(b => b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null && l.BookId == b.Id) >= 1),
                CurrentStateBooksCount = context.Books.Count(),
                CurrentStateLoanedBooksCount = context.Loans.Where(l => l.ReturnedDate == null).Sum(l => l.Books.Count()),


                Year = year,
                Books = context.Books.Where(b => b.CreatedAt >= startDate && b.CreatedAt < endDate).Select(b => new BookResponse()
                {
                    Id = b.Id,
                    Name = b.Name,
                    CreatedAt = b.CreatedAt,
                    Authors = b.Authors.Select(a => new BookAuthorResponse(){
                        Author = new()
                        {
                            FullName = a.Author.FullName
                        }
                    }).ToList(),
                    Genres = b.Genres.Select(g => new GenreBookResponse()
                    {
                        Genre = new(){
                            Name = g.Genre.Name
                        }
                    }).ToList(),
                    LoansCount = b.Loans.Count(),
                    ActiveLoansCount = b.Loans.Count(),
                    Stock = b.Stock,
                    AvailableAmount = b.AvailableAmount
                }).ToList(),
                
                CurrentStateUnactiveReaders = context.Readers.Count(l => l.Loans.All(a => a.ReturnedDate != null)),
                CurrentStateActiveReaders = context.Readers.Count(l => l.Loans.Any(r => r.ReturnedDate == null)),
                CurrentStateReadersCount = context.Readers.Count(),
                CurrentStateReadersWithExceededLoansCount = context.Readers.Select(r => r.Loans.Where(l => l.DueDate < DateTime.UtcNow)).Count(),
                Readers = context.Readers.Where(b => b.CreatedAt >= startDate && b.CreatedAt < endDate).Select(l => new ReaderResponse()
                {
                    Id = l.Id,
                    FullName = l.FullName,
                    StudentLicence = l.StudentLicence ?? "",
                    IdentityCardNumber = l.IdentityCardNumber ?? "",
                    Passport = l.Passport ?? "",
                    LoansCount = l.Loans.Count()

                }).ToList(),

              
                CurrentSatetLoansCount = context.Loans.Count(),
                CurrentStateExceededCount = context.Loans.Count(l => l.ReturnedDate == null && l.DueDate < DateTime.UtcNow),
                CurrentStateNotReturnedLoansCount = context.Loans.Count(r => r.ReturnedDate == null),
                CurrentStateReturnedLoansCount = context.Loans.Count(r => r.ReturnedDate != null),
                Loans = context.Loans.Where(b => b.CreatedAt >= startDate && b.CreatedAt < endDate).Select(l => new LoanResponse()
                {
                    Id = l.Id,
                    CreatedAt = l.CreatedAt,
                    ReturnedDate = l.ReturnedDate,
                    DueDate = l.DueDate,
                    BookCount = l.Books.Count(),
                    Reader = new ReaderResponse(){

                        FullName = l.Reader.FullName,
                        StudentLicence = l.Reader.StudentLicence ?? "",
                        IdentityCardNumber = l.Reader.IdentityCardNumber ?? "",
                        Passport = l.Reader.Passport ?? ""
                    }


                    

                }).ToList(),

                 CurrentStatePayedPenalties = context.Penalties.Count(p => !p.IsDue),
                CurrentStateUnpayedPenalties = context.Penalties.Count(p => p.IsDue),
                CurrentStateTotalPenalties = context.Penalties.Count(),
                Penalties = context.Penalties.Where(b => b.CreatedAt >= startDate && b.CreatedAt < endDate).Select(l => new PenaltyResponse()
                {
                    Id = l.Id,
                    CreatedAt = l.CreatedAt,
                    IsDue = l.IsDue,
                    TransactionId = l.TransactionId,
                    DailyFineRate = l.DailyFineRate,
                    OverdueDays = l.OverdueDays,
                    TotalAmount = l.TotalAmount

                   


                    

                }).ToList(),

            }).FirstOrDefaultAsync() ?? new();
        }
    }
}