using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Client.Responses
{
    public class BookResponse : BaseResponse
    {
        [MaxLength(100), MinLength(1), Required]
        public string Name { get; set; } = null!;
        
        [Url]
        public string? CoverUrl { get; set; }
        
        [MaxLength(500), MinLength(10), Required]
        public string Synopsis { get; set; } = null!;
        
        public List<BookAuthorResponse> Authors { get; set; } = new();
        
        public List<GenreBookResponse> Genres { get; set; } = new();
        
        public int Stock { get; set; }

        public int AvailableAmount { get; set; }

    }
}