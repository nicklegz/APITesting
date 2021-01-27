using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechnicalAnalysisApp_Api.Models
{
    public class Equity
    {
        //maps to symbol
        [Key]
        public string TickerId { get; set; }

        public string CompanyName { get; set; }

        public string Industry { get; set; }

    }
}
