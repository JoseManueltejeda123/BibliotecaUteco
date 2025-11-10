using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Client.Responses
{
    public class ApplicationSummaryResponse
    {
     
        public BooksSummaryResponse BooksSummary { get; set; } = new();
        public LoanSummaryResponse LoansSummary { get; set; } = new();
    }

    public class BooksSummaryResponse
    {
        public int AvailableBooks { get; set; }
        public int LoanedBooks { get; set; }
        public int NonAvailableBooks { get; set; }
        public int TotalBooksCount { get; set; }
    }
    
    public class LoanSummaryResponse
    {
        public int ActiveLoans { get; set; }
        public int ReturnedLoans { get; set; }
        public int NonReturnedLoans { get; set; }
        public int ExceededLoans { get; set; }
        public int TotalLoansCount { get; set; }
        
    }
}