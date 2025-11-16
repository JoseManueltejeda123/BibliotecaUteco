namespace BibliotecaUteco.Client.Responses;

public class TopBooksResponse
{
    public int Month {get; set;}
    public int Year {get; set;}
    public List<BookResponse> Books { get; set; } = new();
}

public class LoansPerMonthResponse
{

    public int Year { get; set; }
    public Dictionary<string, int> MonthLoanCount { get; set; } = new();
}

public class GeneralReport
{

    //books
    public int Year {get; set;}
    public int Month {get; set;}

    public int CurrentStateBooksCount {get; set;}
    public int CurrentStateAvailableBooksCount {get; set;}

    public int CurrentStateLoanedBooksCount {get; set;}

    public List<BookResponse> Books {get; set;} = new();


    //readers

    public int CurrentStateReadersCount {get; set;}

    public int CurrentStateActiveReaders {get; set;}

    public int CurrentStateUnactiveReaders {get; set;}

    public int CurrentStateReadersWithExceededLoansCount {get; set;}

    public List<ReaderResponse> Readers {get; set;} = new();

    //loans

    public int CurrentSatetLoansCount {get; set;}
    public int CurrentStateExceededCount {get; set;}

    public int CurrentStateReturnedLoansCount {get; set;}
    public int CurrentStateNotReturnedLoansCount {get; set;}

    public List<LoanResponse> Loans {get; set;} = new();


    //penalties
    

    public int CurrentStateTotalPenalties {get; set;}
    public int CurrentStateUnpayedPenalties {get; set;}
    public int CurrentStatePayedPenalties {get; set;}

    public List<PenaltyResponse> Penalties {get; set;} = new();

    //penalties
    

    public double CurrentStateCashBox {get; set;}
    public int CurrentStateTrasactionsCount {get; set;}

    public List<TransactionResponse> Transactions {get; set;} = new();


}
