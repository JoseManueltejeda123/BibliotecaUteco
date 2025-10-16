namespace BibliotecaUteco.Client.Responses
{
    public class AuthorResponse : BaseResponse
    {

        public string FullName { get; set; } = "";
        public int BooksCount { get; set; } = 0;  
    }
}