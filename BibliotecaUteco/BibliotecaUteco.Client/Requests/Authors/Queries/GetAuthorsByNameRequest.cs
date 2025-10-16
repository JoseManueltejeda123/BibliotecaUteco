using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Authors.Queries
{
    public class GetAuthorsByNameRequest
    {
        [MaxLength(30, ErrorMessage = "El nombre del autor no puede exceder los 30 caracteres.")]
        public string AuthorsName { get; set; } = "";
    }
}