using FetchHtml.Models;
using System.Collections;
using System.Collections.Generic;

namespace FetchHtml
{
    public interface IHtmlHelper<in T> where T : QueryModel
    {
        IEnumerable<QueryResult> Execute(T t);

    }


}