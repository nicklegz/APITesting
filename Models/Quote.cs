using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TechnicalAnalysisApp_Api.Models
{
    public class Quote
    {
        [Key]
        public string QuoteId { get; set; }

        public string TickerId { get; set; }

        public decimal OpenPrice { get; set; }

        public decimal ClosePrice { get; set; }

        public decimal HighPriceOfDay { get; set; }

        public decimal LowPriceOfDay { get; set; }

        public int AvgTotalVol { get; set; }

        public decimal Week52High { get; set; }

        public decimal Week52Low { get; set; }

        public string Date { get; set; }

        public decimal YTDChange { get; set; }
    }
}
