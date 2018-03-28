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

            var agricultureHtmlHelper = new AgricultureHtmlHelper();

            var queryResult = agricultureHtmlHelper.Execute(new MainPageQueryModel()
            {
                Url = "http://jiuban.moa.gov.cn/zwllm/jcyj/",
                Title = "中华人民共和国农业部",
                KeyWords = new string[] { "生猪存栏", "生猪屠宰", "农产品进出口" },
            });


            var result = queryResult.ToList();

            string filePath = Path.Combine(Environment.CurrentDirectory, "result.txt");
            File.Delete(filePath);
            //FileStream fileStream = File.Open(, FileMode.OpenOrCreate);
            foreach (var item in result)
            {
                string[] arr = new string[] {
                    "============================================================================",
                    item.Title,
                    item.Body
                };
                File.AppendAllLinesAsync(filePath, arr, Encoding.UTF8).Wait();
            }

            Console.WriteLine("===========完成===========");
            Console.ReadLine();

        }
    }
}
