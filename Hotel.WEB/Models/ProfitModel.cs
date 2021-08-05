using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models
{
    public class ProfitModel
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public double Profit_For_Month { get; set; }
    }
}