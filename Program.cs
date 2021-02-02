using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechnicalAnalysisApp_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using TechnicalAnalysisApp_Api.Json;

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
            string pathParam = "";

            WatchlistJson watchlist = new WatchlistJson();
            var watchlistEquities = watchlist.GetWatchlistJson();

            if (watchlistEquities != null)
            {
                foreach (var item in watchlistEquities.Equities)
                {
                    pathParam = string.Format("stock/{0}/book", item.Equity);

                    string uri = helpers.UriHelper(baseUrl, pathParam);

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

                    var result = JsonSerializer.Deserialize<JsonRootResponse>(body);

                    using (var context = new StockDataContext())
                    {
                        //if Equity already exists in database, only add row to Quote table
                        var newEquityQuery = context.Equities.Where(e => e.CompanyName == result.Quote.CompanyName).FirstOrDefault<Equity>();

                        if (newEquityQuery == null)
                        {
                            Equity equity = new Equity()
                            {
                                CompanyName = result.Quote.CompanyName,
                                Industry = result.Quote.Industry,
                                TickerId = result.Quote.Symbol
                            };

                            context.Equities.Add(equity);
                        }

                        var newQuoteQuery = context.Quotes.Where(q => q.TickerId == result.Quote.Symbol && q.Date == result.Quote.Date).FirstOrDefault<Quote>();

                        if (newQuoteQuery == null)
                        {
                            Quote quote = new Quote()
                            {
                                TickerId = result.Quote.Symbol,
                                OpenPrice = result.Quote.OpenPrice,
                                ClosePrice = result.Quote.ClosePrice,
                                HighPriceOfDay = result.Quote.HighPriceOfDay,
                                LowPriceOfDay = result.Quote.LowPriceOfDay,
                                AvgTotalVol = result.Quote.AvgTotalVol,
                                Week52High = result.Quote.Week52High,
                                Week52Low = result.Quote.Week52Low,
                                Date = result.Quote.Date,
                                YTDChange = result.Quote.YTDChange,
                                QuoteId = Guid.NewGuid().ToString()
                            };

                            context.Quotes.Add(quote);
                        }

                        context.SaveChanges();
                    }
                }
            }
        }
    }
}


