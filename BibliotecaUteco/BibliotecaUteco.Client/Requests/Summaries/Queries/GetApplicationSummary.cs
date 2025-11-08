using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Client.Requests.Summaries.Queries
{
    public class GetApplicationSummary
    {
        public DateTime StartTime {get; set;} = DateTime.Now;
        public DateTime EndTime {get; set;} = DateTime.Now;
       
    }

    
}