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
            SetUserToUpdate();
            SetCreatedReader();
            SetReaderToUpdate();
            SetUpdatedReader();
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
     public UserResponse? UserToUpdate { get; set; } = null;
    
        public event Action? OnUserToUpdateChanged ;
    
        public void SetUserToUpdate(UserResponse? user = null)
        {
            UserToUpdate = user;
            if (user is null) return;
            OnUserToUpdateChanged?.Invoke();
        }
        public ReaderResponse? CreatedReader { get; set; } = null;
    
        public event Action? OnCreatedReaderChanged ;
    
        public void SetCreatedReader(ReaderResponse? reader = null)
        {
            CreatedReader = reader;
            if (reader is null) return;
            OnCreatedReaderChanged?.Invoke();
        }
        
         public ReaderResponse? ReaderToUpdate { get; set; } = null;
         
        public event Action? OnReaderToUpdateChanged ;
    
        public void SetReaderToUpdate(ReaderResponse? reader = null)
        {
            ReaderToUpdate = reader;
            if (reader is null) return;
            OnReaderToUpdateChanged?.Invoke();
        }
        
        
        public ReaderResponse? UpdatedReader { get; set; } = null;
         
        public event Action? OnUpdatedReaderChanged ;
    
        public void SetUpdatedReader(ReaderResponse? reader = null)
        {
            UpdatedReader = reader;
            if (reader is null) return;
            OnUpdatedReaderChanged?.Invoke();
        }
}

public enum RightBarView
{
    Default,
    CreatingBook,
    UpdateBook,
    BookDetails,
    Users,
    CreateUser,
    UpdateUser,
    CreateReader,
    EditReader,
    Authors
}