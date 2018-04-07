using FetchHtml.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;
using FetchHtml.BLL;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FetchHtml
{
    class Program
    {
        static void Main(string[] args)
        {

            //IHtmlHelper htmlHelper = new AgricultureHtmlHelper();

            //var queryResult = htmlHelper.Execute(new MainPageQueryModel()
            //{
            //    Url = "http://jiuban.moa.gov.cn/zwllm/jcyj/",
            //    Title = "中华人民共和国农业部",
            //    KeyWords = new string[] { "生猪存栏", "生猪屠宰", "农产品进出口" },
            //});


            //var result = queryResult.GetAwaiter().GetResult().ToList();

            string urlTemplate = "http://nufm.dfcfw.com/EM_Finance2014NumericApplication/JS.aspx?type=CT&cmd={0}&sty=FCFL4O&token={1}";
            string token = "7bc05d0d4c3c22ef9fca8c2a912d779c";
            string[] cmds = new string[] {
                "C.SHFE",//上期所
                "C.F_INE_SC",//上期能源
                "C.DCE",//大商所
                "C.CZCE",//郑商所
                "C._168_FO",//中金所
                "C._UF",//国际期货
            };

            IEnumerable<string> urls = cmds.Select(t => string.Format(urlTemplate, t, token));

            List<Task<JArray>> tasks = new List<Task<JArray>>();

            EastMoneyJsonHelper jsonHelper = new EastMoneyJsonHelper();

            foreach (var item in urls)
            {
                var task = jsonHelper.Fetch(item);
                tasks.Add(task);
            }

            var taskArray = tasks.Cast<Task>().ToArray();
            Task.WaitAll(taskArray);

            List<JToken> list = new List<JToken>();

            foreach (Task<JArray> task in tasks)
            {
                list.AddRange(task.Result);
            }


            jsonHelper.SaveResultToDb(list).Wait();

            Console.WriteLine("===========Completed===========");
            Console.ReadLine();

        }
    }
}
