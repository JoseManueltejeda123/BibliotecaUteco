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

    }
}