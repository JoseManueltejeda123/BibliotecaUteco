using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.Stores;

public record RightBarState
{
    public RightBarView View { get; init; } = RightBarView.Default;
    public BookResponse? CreatedBook { get; init; }
    public BookResponse? BookToUpdate { get; init; }
    public BookResponse? UpdatedBook { get; init; }
    public BookResponse? BookDetails { get; init; }
    public UserResponse? UserToUpdate { get; init; }
    public ReaderResponse? CreatedReader { get; init; }
    public ReaderResponse? ReaderToUpdate { get; init; }
    public ReaderResponse? UpdatedReader { get; init; }
    public static RightBarState Empty => new();

    public RightBarState ClearData() =>
        this with
        {
            CreatedBook = null,
            BookToUpdate = null,
            UpdatedBook = null,
            BookDetails = null,
            UserToUpdate = null,
            CreatedReader = null,
            ReaderToUpdate = null,
            UpdatedReader = null,
        };
}

public class RightBarStore : IDisposable
{
    private RightBarState _state = RightBarState.Empty;
    private readonly SemaphoreSlim _stateLock = new(1, 1);

    public RightBarState State => _state;

    // Un solo evento con el estado completo
    public event Action<RightBarState>? OnStateChanged;

    public async Task UpdateStateAsync(Func<RightBarState, RightBarState> updater)
    {
        await _stateLock.WaitAsync();
        try
        {
            var oldState = _state;
            _state = updater(_state);

            if (!ReferenceEquals(oldState, _state))
            {
                OnStateChanged?.Invoke(_state);
            }
        }
        finally
        {
            _stateLock.Release();
        }
    }

    // Métodos helper
    public Task SetViewAsync(RightBarView view)
    {
        return UpdateStateAsync(s =>
        {
            var newState = s with { View = view };
            return view == RightBarView.Default ? newState.ClearData() : newState;
        });
    }

    public Task SetCreatedBookAsync(BookResponse? book) =>
        UpdateStateAsync(s => s with { CreatedBook = book });

    public Task SetUpdatedBookAsync(BookResponse? book) =>
        UpdateStateAsync(s => s with { UpdatedBook = book });

    public Task SetCreatedReaderAsync(ReaderResponse? reader) =>
        UpdateStateAsync(s => s with { CreatedReader = reader });

    public Task SetUpdatedReaderAsync(ReaderResponse? reader) =>
        UpdateStateAsync(s => s with { UpdatedReader = reader });

    public Task OpenCreateBookAsync() =>
        UpdateStateAsync(s => s.ClearData() with { View = RightBarView.CreatingBook });

    //batched
    public Task OpenUpdateBookAsync(BookResponse book) =>
        UpdateStateAsync(s =>
            s.ClearData() with
            {
                View = RightBarView.UpdateBook,
                BookToUpdate = book,
            }
        );

    public Task OpenUpdateUserAsync(UserResponse user) =>
        UpdateStateAsync(s =>
            s.ClearData() with
            {
                View = RightBarView.UpdateUser,
                UserToUpdate = user,
            }
        );

    public Task OpenUpdateReaderAsync(ReaderResponse reader) =>
        UpdateStateAsync(s =>
            s.ClearData() with
            {
                View = RightBarView.UpdateReader,
                ReaderToUpdate = reader,
            }
        );

    public Task OpenCreateReaderAsync() =>
        UpdateStateAsync(s => s.ClearData() with { View = RightBarView.CreateReader });

    public Task OpenCreateUserAsync() =>
        UpdateStateAsync(s => s.ClearData() with { View = RightBarView.CreateUser });

    public Task OpenBookDetailsAsync(BookResponse book) =>
        UpdateStateAsync(s =>
            s.ClearData() with
            {
                View = RightBarView.BookDetails,
                BookDetails = book,
            }
        );

    public Task CloseAsync() => UpdateStateAsync(s => RightBarState.Empty);

    public void Dispose()
    {
        _stateLock?.Dispose();
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
    UpdateReader,
    Authors,
}
