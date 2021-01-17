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

namespace APITesting
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main()
        {
            //read .txt file containing the JSON text
            var rawJSON = File.ReadAllText(@"C:\Users\L3GZ\source\repos\APITesting\APITesting\Test Files\JSONResponse.txt");

            //extract JSONElement based on the Stock Data class
            var deserial = JsonSerializer.Deserialize<StockData>(rawJSON);

            //query JSONElement for exchange equal to Germany
            var query = from item in deserial.Quotes.EnumerateArray()
                        where item.GetProperty("exchange").ToString() == "GER"
                        select item;

            foreach(var result in query)
            {
                Console.WriteLine(result);
            }


            /*
            string baseUri = "https://apidojo-yahoo-finance-v1.p.rapidapi.com/auto-complete";
            List<string> filter = new List<string>() { "q=microsoft", "region=US" };

            string newUri = UriHelper(baseUri, filter);
            //Console.WriteLine(newUri);
            //Console.ReadLine();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(newUri),
                Headers = {
                    { "x-rapidapi-key", "9b10f8dd27mshf3a33f7fe54e3bcp11bf99jsnd93d02395254" },
                    { "x-rapidapi-host", "apidojo-yahoo-finance-v1.p.rapidapi.com" }}
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
            */
        }
        static string UriHelper(string fragment, List<string> filters)
        {
            UriBuilder uri = new UriBuilder(fragment);

            string concatQuery = "";
            
            //build query substring based on List
            if(filters.Count > 0)
            {
                foreach(string filter in filters)
                {
                    if(concatQuery == "")
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
