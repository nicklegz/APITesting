using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace APITesting
{
    public class StockData
    {
        [JsonPropertyName("quotes")]
        public JsonElement Quotes { get; set; }

    }
}
