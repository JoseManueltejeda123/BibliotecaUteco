namespace BibliotecaUteco.Client.Responses
{
    public class GenreBookResponse : BaseResponse
    {
        public GenreResponse Genre { get; set; } = new();

        public int GenreId { get; set; }
        
        public int BookId { get; set; }
    }
}