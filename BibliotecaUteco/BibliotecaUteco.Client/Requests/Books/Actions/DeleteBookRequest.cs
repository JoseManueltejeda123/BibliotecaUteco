using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Books.Actions
{
    public class DeleteBookRequest
    {
        [Required, Range(0, int.MaxValue, ErrorMessage = "El valor del id del libro debe de ser mayor a 1")]
        public int BookId { get; set; }
    }
}