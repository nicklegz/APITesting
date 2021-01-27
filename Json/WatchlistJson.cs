using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TechnicalAnalysisApp_Api.Json
{
    public class WatchlistJson
    {
        private readonly string watchlistFilePath = ConfigurationManager.AppSettings["watchlistFilePath"];
        public WatchlistJson GetWatchlistJson()
        {
            var deserializeWatchlist = JsonSerializer.Deserialize<WatchlistJson>(File.ReadAllText(watchlistFilePath));
            return deserializeWatchlist;
        }

        [JsonPropertyName("Equities")]
        public List<WatchListEquity> Equities { get; set; }
    }

    public class WatchListEquity
    {
        [JsonPropertyName("Equity")]
        public string Equity { get; set; }
    }
}
