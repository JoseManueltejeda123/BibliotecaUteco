namespace BibliotecaUteco.Client.Responses
{
    public class BookAuthorResponse : BaseResponse
    {
        public AuthorResponse Author { get; set; } = new();
        public int BookId { get; set; } 

        public int AuthorId { get; set; } 
    }
}