using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace APITesting
{
    public class JsonRootResponse
    {
        [JsonPropertyName("quote")]
        public JsonBodyResponse Quote { get; set; }
    }

    public class JsonBodyResponse
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }

        [JsonPropertyName("sector")]
        public string Industry { get; set; }

        [JsonPropertyName("open")]
        public decimal OpenPrice { get; set; }

        [JsonPropertyName("delayedPrice")]
        public decimal ClosePrice { get; set; }

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
