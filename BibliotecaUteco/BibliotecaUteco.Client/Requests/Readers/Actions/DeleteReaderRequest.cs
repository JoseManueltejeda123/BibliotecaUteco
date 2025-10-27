using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Client.Requests.Readers.Actions
{
    public class DeleteReaderRequest
    {
        [
            Range(1, int.MaxValue, ErrorMessage = "El ID debe de ser mayor a 1"),
            Required(ErrorMessage = "El ID del usuario a eliminar es obligatorio")
        ]
        public int ReaderId { get; set; }
    }
}
