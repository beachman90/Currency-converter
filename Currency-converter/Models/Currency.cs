using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency_converter.Models
{
    public static class CurrencyList
    {
        public static readonly List<string> prioCurrencies = new List<string> 
        {
            "EUR", "USD", "NOK", "GBP", "SEK", "DKK", "JPY", "CHF", "AUD", "CAD"
        };
    }

    public class CurrencyResponse
    {
        public bool success { get; set; }
        public bool historical { get; set; }
        public Dictionary<string, decimal> rates { get; set; }
    }
}
