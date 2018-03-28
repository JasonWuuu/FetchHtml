using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FetchHtml.Models;
using HtmlAgilityPack;
using System.Linq;

namespace FetchHtml
{
    public class AgricultureHtmlHelper : IHtmlHelper<MainPageQueryModel>
    {
        public IEnumerable<QueryResult> Execute(MainPageQueryModel queryModel)
        {

            ExecuteMainPage(queryModel);

            return ExecuteSubPage(queryModel);
        }

        public void ExecuteMainPage(MainPageQueryModel queryModel)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument document = htmlWeb.Load(queryModel.Url, "GET");

            HtmlNodeCollection hrefs = document.DocumentNode.SelectNodes("//div[@class='zleft']//a[@href]");

            foreach (HtmlNode item in hrefs)
            {
                string title = item.Attributes["title"] == null ? string.Empty : item.Attributes["title"].Value;

                bool isMatchedKeyWord = queryModel.KeyWords.Any(keyword => title.Contains(keyword));

                if (isMatchedKeyWord)
                {
                    string value = item.Attributes["href"].Value.TrimStart('.', '/');
                    string path = Path.Combine(queryModel.Url, value);

                    queryModel.SubPageQueryModels.Add(new SubPageQueryModel()
                    {
                        Url = path,
                        Title = title
                    });
                }
            }
        }

        public IEnumerable<QueryResult> ExecuteSubPage(MainPageQueryModel queryModel)
        {
            List<QueryResult> queryResultList = new List<QueryResult>();

            HtmlWeb htmlWeb = new HtmlWeb();
            foreach (SubPageQueryModel subModel in queryModel.SubPageQueryModels)
            {
                HtmlDocument document = htmlWeb.Load(subModel.Url, "GET");
                HtmlNode titleNode = document.DocumentNode.SelectSingleNode("//div[@class='zleft']/h1");

                HtmlNode bodyNode = document.DocumentNode.SelectSingleNode("//div[@class='zlcont']/div[@id='TRS_AUTOADD']");

                queryResultList.Add(new QueryResult()
                {
                    Title = titleNode.InnerText,
                    Body = bodyNode.InnerHtml,
                    Url = subModel.Url
                });
            }

            return queryResultList;
        }
    }
}
