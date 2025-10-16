namespace BibliotecaUteco.Client.Requests.Books.Queries
{
    public class GetBooksByFilterRequest
    {
        public string? BookName { get; set; }
        public string? GenreName { get; set; }
        public string? AuthorName { get; set; }
        public int Take { get; set; } = 10;
        public int Skip { get; set; }
    }
}