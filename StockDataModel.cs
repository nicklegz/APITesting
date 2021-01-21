using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TechnicalAnalysisApp_Api
{
    public class StockDataModel
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("high")]
        public decimal HighPriceOfDay { get; set; }

        [JsonPropertyName("low")]
        public decimal LowPriceOfDay { get; set; }

        [JsonPropertyName("avgTotalVolume")]
        public int AvgTotalVol { get; set; }

        [JsonPropertyName("week52High")]
        public decimal Week52High { get; set; }

        [JsonPropertyName("week52Low")]
        public decimal Week52Low { get; set; }

        [JsonPropertyName("latestTime")]
        public string Date { get; set; }

        [JsonPropertyName("ytdChange")]
        public decimal YTDChange { get; set; }
    }
}
