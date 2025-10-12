using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Client.Requests.Books.Actions
{
    public class DeleteBookRequest
    {
        [Required, Range(0, int.MaxValue, ErrorMessage = "El valor del id del libro debe de ser mayor a 1")]
        public int BookId { get; set; }
    }
}