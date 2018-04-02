using FetchHtml.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FetchHtml
{
    class Program
    {
        static void Main(string[] args)
        {

            IHtmlHelper htmlHelper = new AgricultureHtmlHelper();

            var queryResult = htmlHelper.Execute(new MainPageQueryModel()
            {
                Url = "http://jiuban.moa.gov.cn/zwllm/jcyj/",
                Title = "中华人民共和国农业部",
                KeyWords = new string[] { "生猪存栏", "生猪屠宰", "农产品进出口" },
            });


            var result = queryResult.GetAwaiter().GetResult().ToList();
            

            Console.WriteLine("===========Completed===========");
            Console.ReadLine();

        }
    }
}
