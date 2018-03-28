using System;
using System.Collections.Generic;
using System.Text;
using FetchHtml.Models;
using HtmlAgilityPack;

namespace FetchHtml
{
    public abstract class BaseHtmlHelper<T>
    {
        public virtual IEnumerable<QueryResult> Execute(T queryModel)
        {
            //擦，没法共用
            return null;
        }
    }
}
