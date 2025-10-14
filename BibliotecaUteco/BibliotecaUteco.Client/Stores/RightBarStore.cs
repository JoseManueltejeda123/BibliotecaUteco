using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.Stores;

public class RightBarStore
{
    public RightBarView View { get; set; } = RightBarView.Default;

    public event Action? OnViewChanged ;

    public void SetView(RightBarView view = RightBarView.Default)
    {
        View = view;
        OnViewChanged?.Invoke();

        if (view == RightBarView.Default)
        {
            SetUpdatedBook();
            SetBookToUpdate();
            SetCreatedBook();
            SetBookDetails();
        }
    }

    public BookResponse? CreatedBook { get; set; } = null;
    
    public event Action? OnCreatedBookChanged ;

    public void SetCreatedBook(BookResponse? book = null)
    {
        CreatedBook = book;
        if (book is null) return;
        OnCreatedBookChanged?.Invoke();
    }
    
    public BookResponse? BookToUpdate { get; set; } = null;
    
    public event Action? OnBookToUpdateChanged ;

    public void SetBookToUpdate(BookResponse? book = null)
    {
        BookToUpdate = book;
        if (book is null) return;
        OnBookToUpdateChanged?.Invoke();
    }
    
    
    
     public BookResponse? UpdatedBook { get; set; } = null;
        
     public event Action? OnUpdatedBookChanged ;
    
        public void SetUpdatedBook(BookResponse? book = null)
        {
            
            UpdatedBook = book;
            if (book is null) return;
            OnUpdatedBookChanged?.Invoke();
        }
        
    public BookResponse? BookDetails { get; set; } = null;

    public event Action? OnBookDetailsChanged ;

    public void SetBookDetails(BookResponse? book = null)
    {
        BookDetails = book;
        if (book is null) return;
        OnBookDetailsChanged?.Invoke();
    }
}

public enum RightBarView
{
    Default,
    CreatingBook,
    UpdateBook,
    BookDetails,
    Users,
    Genres,
    Authors
}