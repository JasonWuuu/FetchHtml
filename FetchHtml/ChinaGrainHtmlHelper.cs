using System;
using System.Collections.Generic;
using System.Text;
using FetchHtml.Models;
using HtmlAgilityPack;

namespace FetchHtml
{
    public class ChinaGrainHtmlHelper : BaseHtmlHelper<SubPageQueryModel>, IHtmlHelper<SubPageQueryModel>
    {
        public override IEnumerable<QueryResult> Execute(SubPageQueryModel queryModel)
        {
            return null;
        }
    }
}
