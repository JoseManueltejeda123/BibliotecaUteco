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
    }

    public BookResponse? CreatedBook { get; set; } = null;
    
    public event Action? OnCreatedBookChanged ;

    public void SetCreatedBook(BookResponse? book = null)
    {
        CreatedBook = book;
        OnCreatedBookChanged?.Invoke();
    }
}

public enum RightBarView
{
    Default,
    CreatingBook
}