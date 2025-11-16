using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUteco.Client.Requests.Reports
{
    public class GetGeneralReportRequest
    {
        public int Year {get; set;} = DateTime.Now.Year;
        public int? Month {get; set;} = DateTime.Now.Month;
    }
}