using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FetchHtml.Models;
using HtmlAgilityPack;
using System.Linq;
using System.Threading.Tasks;

namespace FetchHtml
{
    public class AgricultureHtmlHelper : BaseHtmlHelper<MainPageQueryModel>, IHtmlHelper<MainPageQueryModel>
    {
        public bool CanExecute(IQueryModel queryModel)
        {
            return queryModel.GetType().Equals(typeof(MainPageQueryModel));
        }

        public Task<IEnumerable<QueryResult>> Execute(IQueryModel queryModel)
        {
            return CanExecute(queryModel) ? Execute((MainPageQueryModel)queryModel) : Task.FromResult<IEnumerable<QueryResult>>(null);

        }

        public Task<IEnumerable<QueryResult>> Execute(MainPageQueryModel queryModel)
        {

            ExecuteMainPage(queryModel).Wait();

            return ExecuteSubPage(queryModel);
        }

        public async Task ExecuteMainPage(MainPageQueryModel queryModel)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument document = await htmlWeb.LoadFromWebAsync(queryModel.Url);

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

        public async Task<IEnumerable<QueryResult>> ExecuteSubPage(MainPageQueryModel queryModel)
        {
            List<QueryResult> queryResultList = new List<QueryResult>();

            HtmlWeb htmlWeb = new HtmlWeb();
            foreach (SubPageQueryModel subModel in queryModel.SubPageQueryModels)
            {
                HtmlDocument document = await htmlWeb.LoadFromWebAsync(subModel.Url);
                HtmlNode titleNode = document.DocumentNode.SelectSingleNode("//div[@class='zleft']/h1");

                HtmlNode bodyNode = document.DocumentNode.SelectSingleNode("//div[@class='zlcont']/div[@id='TRS_AUTOADD']");

                queryResultList.Add(new QueryResult()
                {
                    Title = titleNode.InnerText,
                    Body = bodyNode.InnerHtml,
                    Url = subModel.Url
                });
            }

            //save to db
            SaveResultToDb(queryResultList).Wait();

            return queryResultList;
        }
    }
}
