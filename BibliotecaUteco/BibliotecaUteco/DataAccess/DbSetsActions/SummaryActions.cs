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
             ApplicationSummaryResponse response = new();
            if (!date.HasValue)
                date = DateTime.UtcNow;

            var summary = await context.Books
             .GroupBy(_ => 1)
             .Select(_ => new ApplicationSummaryResponse
             {
                 
                    
                BooksSummary = new BooksSummaryResponse
                {
                    AvailableBooks = context.Books.Count(b => b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null && l.BookId == b.Id) >= 1),
                    LoanedBooks = context.Loans.Count(),
                    NonAvailableBooks = context.Books.Count(b => b.Stock - b.Loans.Count(l => l.Loan.ReturnedDate == null && l.BookId == b.Id) <= 0),
                    TotalBooksCount = context.Books.Count()
                },

                LoansSummary = new LoanSummaryResponse
                {
                    ActiveLoans = context.Loans.Count(l => l.ReturnedDate == null),
                    ReturnedLoans = context.Loans.Count(l => l.ReturnedDate != null),
                    ExceededLoans = context.Loans.Count(l => l.DueDate < DateTime.UtcNow && l.ReturnedDate == null),
                    TotalLoansCount = context.Loans.Count()
                }
             })
             .FirstAsync();

            return summary;
        }
    }
}