using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FetchHtml.BLL
{
    public class HttpClientHelper
    {
        public async Task<string> Request(string url, string method = "GET")
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {

                    HttpRequestMessage httpRequestMessage = new HttpRequestMessage(new HttpMethod(method), url);
                    HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        return await httpResponseMessage.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                throw;
            }
            
        }

        
    }
}
