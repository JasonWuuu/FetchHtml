using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace FetchHtml.BLL
{
    /// <summary>
    /// 东方财富网，获取期货数据
    /// </summary>
    public class EastMoneyJsonHelper
    {
        public async Task<JArray> Fetch(string url)
        {
            HttpClientHelper httpClientHelper = new HttpClientHelper();

            string body = await httpClientHelper.Request(url);

            body = body.Trim('(', ')');

            JArray jArray = JsonConvert.DeserializeObject(body) as JArray;

            //await SaveResultToDb(jArray);

            return jArray;

        }

        public async Task<bool> SaveResultToDb(IEnumerable<JToken> jArray)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "result.txt");
            File.Delete(filePath);
            //FileStream fileStream = File.Open(, FileMode.OpenOrCreate);
            //foreach (JToken item in jArray)
            //{
            //    string[] arr = new string[] {

            //        item.ToString()
            //    };
               
            //}

            await File.AppendAllLinesAsync(filePath, jArray.Select(t => t.ToString()), Encoding.UTF8);

            return true;
        }
    }
}
