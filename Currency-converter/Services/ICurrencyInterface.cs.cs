using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency_converter.Services
{
    public interface ICurrencyService
    {
        Task<decimal> ConvertCurrency(string fromCurrency, string toCurrency, decimal amount);
        Task<decimal> ConvertCurrencyHistorical(DateTime date, string fromCurrency, string toCurrency, decimal amount);
        Task StoreDailyRates();
    }
}
