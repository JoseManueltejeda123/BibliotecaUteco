using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Client.Responses
{
    public class AuthorResponse : BaseResponse
    {

        public string Name { get; set; } = "";
        public int BooksCount { get; set; } = 0;  
    }
}