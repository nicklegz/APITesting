using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using TechnicalAnalysisApp_Api;

namespace APITesting
{
    public class Helpers
    {
        public StockDataModel FormatJson(string rawJson)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            return JsonSerializer.Deserialize<StockDataModel>(rawJson);

            // JsonSerializer.Serialize(deserialize, options);
        }

        public string UriHelper(string fragment, List<string> filters, string path)
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
