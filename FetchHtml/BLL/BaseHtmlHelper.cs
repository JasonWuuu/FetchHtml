using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FetchHtml.Models;
using HtmlAgilityPack;

namespace FetchHtml.BLL
{ 

    public abstract class BaseHtmlHelper<T>
    {
        //public virtual Task<IEnumerable<QueryResult>> Execute(T queryModel)
        //{
        //    //擦，没法共用
        //    return Task.FromResult<IEnumerable<QueryResult>>(null);
        //}

        protected async virtual Task<bool> SaveResultToDb(IEnumerable<QueryResult> queryResultList)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "result.txt");
            File.Delete(filePath);
            //FileStream fileStream = File.Open(, FileMode.OpenOrCreate);
            foreach (var item in queryResultList)
            {
                string[] arr = new string[] {
                    "============================================================================",
                    item.Title,
                    item.Body
                };
                await File.AppendAllLinesAsync(filePath, arr, Encoding.UTF8);
            }

            return true;
        }

        
    }
}
