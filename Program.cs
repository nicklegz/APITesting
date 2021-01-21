using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using System.Configuration;
using TechnicalAnalysisApp_Api;

namespace APITesting
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static readonly Helpers helpers = new Helpers();
        static async Task Main()
        {
            string apiHost = ConfigurationManager.AppSettings["apihost"];
            string apiKey = ConfigurationManager.AppSettings["apiKey"];
            string baseUrl = ConfigurationManager.AppSettings["baseurl"];
            string symbol = ConfigurationManager.AppSettings["stocksymbol"];

            string pathParam = String.Format("stock/{0}/book", symbol);

            //can add query parameters as needed
            List<string> filter = new List<string>() /*{ "q=microsoft", "region=US" }*/;

            string uri = helpers.UriHelper(baseUrl, filter, pathParam);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers =
            {
                { "x-rapidapi-key", apiKey},
                { "x-rapidapi-host", apiHost}
            }
            };

            string body;

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }


            var jsonReponse = helpers.FormatJson(body).Symbol;

            Console.WriteLine("Symbol: " + jsonReponse);
            Console.ReadLine();


            //string dateTime = DateTime.Now.ToString().Replace(":", "-").Replace("/","-");
            //string fileName = String.Format(@"S:\Test\{0} - Stock - {1}.txt", dateTime, symbol.ToUpper());

            //await File.WriteAllTextAsync(fileName, formattedJson);

        }
        
    }
}


