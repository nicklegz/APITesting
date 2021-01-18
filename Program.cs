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

namespace APITesting
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main()
        {
            //read.txt file containing the JSON text
            //var rawJSON = File.ReadAllText(@"C:\Users\L3GZ\source\repos\APITesting\APITesting\Test Files\JSONResponse.txt");

            //extract JSONElement based on the Stock Data class
            //var deserial = JsonSerializer.Deserialize<StockData>(rawJSON);

            ////query JSONElement for exchange equal to Germany
            //var query = from item in deserial.Quote.EnumerateArray()
            //            where item.GetProperty("exchange").ToString() == "GER"
            //            select item;


            string apiHost = ConfigurationManager.AppSettings["apihost"];
            string apiKey = ConfigurationManager.AppSettings["apiKey"];
            string baseUrl = ConfigurationManager.AppSettings["baseurl"];
            string symbol = ConfigurationManager.AppSettings["stocksymbol"];

            string pathParam = String.Format("stock/{0}/book", symbol);

            List<string> filter = new List<string>() /*{ "q=microsoft", "region=US" }*/;

            string uri = UriHelper(baseUrl, filter, pathParam);
            //Console.WriteLine(newUri);
            //Console.ReadLine();

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

            string body = "";

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }

            var convertJson = JsonSerializer.Deserialize<StockData>(body);
        }


            static string UriHelper(string fragment, List<string> filters, string path)
            {
                UriBuilder uri = new UriBuilder(fragment);
                uri.Path = path;
                uri.Port = -1;

                string concatQuery = "";

                //build query substring based on List
                if (filters.Count > 0)
                {
                    foreach (string filter in filters)
                    {
                        if (concatQuery == "")
                        {
                            concatQuery = filter;
                        }
                        else
                        {
                            concatQuery = concatQuery + '&' + filter;
                        }
                    }

                    uri.Query = concatQuery;
                }

                return uri.ToString();
        }
    }
}


