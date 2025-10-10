using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Client.Responses
{
    public class GenreBookResponse : BaseResponse
    {
        public GenreResponse Genre { get; set; } = new();

        public int GenreId { get; set; }
        
        public int BookId { get; set; }
    }
}