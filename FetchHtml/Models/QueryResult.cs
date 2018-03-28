using System;
using System.Collections.Generic;
using System.Text;

namespace FetchHtml.Models
{
    public class QueryResult
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTimeOffset CreatedDateTime { get; private set; }
        public string Body { get; set; }
    }
}
