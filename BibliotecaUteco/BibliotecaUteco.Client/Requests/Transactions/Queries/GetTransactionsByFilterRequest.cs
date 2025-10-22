using System.ComponentModel.DataAnnotations;

namespace BibliotecaUteco.Client.Requests.Transactions.Queries;

public class GetTransactionsByFilterRequest
{
         [Range(0, int.MaxValue)]
         public int? UserId { get; set; } = null;
         [Range(0, int.MaxValue)]
        public int Skip { get; set; } = 0;
        
        [Range(1, 10)]
        public int Take { get; set; } = 10;
}