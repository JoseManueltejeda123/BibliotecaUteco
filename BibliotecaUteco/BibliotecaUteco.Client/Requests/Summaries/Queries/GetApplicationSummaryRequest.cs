using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MudBlazor.Extensions;

namespace BibliotecaUteco.Client.Requests.Summaries.Queries
{
    public class GetApplicationSummaryRequest
    {
        public DateTime _date { get; set; } = DateTime.Now;
        public string Date => _date.ToIsoDateString();
        public bool IsPrecise { get; set; } = false;

       
    }

    
}