using Currency_converter.Data;
using Currency_converter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Currency_converter.Services
{
    public class CurrencyService
    {

        /// <summary>
        /// Takes ten prio currencies, shows daily rate relative to EUR and saves it to db once a day.
        /// </summary>
        /// <returns></returns>
        public static async Task StoreDailyRates()
        {
            try
            {
                using (HttpClient client = new())
                {
                    string response = await client.GetStringAsync(
                        $"http://data.fixer.io/api/latest?access_key=1da4394e64da8f692a593dbaae306f28&symbols={string.Join(",", CurrencyList.prioCurrencies)}");

                    var data = JsonSerializer.Deserialize<CurrencyResponse>(response);

                    if (data.success && data.rates != null)
                    {
                        using (CurrencyContext context = new())
                        {
                            foreach (string currency in CurrencyList.prioCurrencies)
                            {
                                if (data.rates.TryGetValue(currency, out decimal rate))
                                {
                                    ExchangeRate exchangeRate = new()
                                    {
                                        Currency = currency,
                                        Rate = rate,
                                        Date = DateTime.UtcNow,
                                    };

                                    context.ExchangeRates.Add(exchangeRate);
                                }
                            }

                            await context.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Detail: {ex.InnerException?.Message}");
                Console.ReadLine();
            }

        }


        public static async Task<decimal> ConvertCurrency(string fromCurrency, string toCurrency, decimal amount)
        {

            if (string.IsNullOrWhiteSpace(fromCurrency) || string.IsNullOrWhiteSpace(toCurrency))
                throw new ArgumentException("Currency codes cannot be empty");

            if (amount <= 0)
                throw new ArgumentException("Amount must be positive");

            using (HttpClient client = new())
            {
                string response = await client.GetStringAsync(
                    "http://data.fixer.io/api/latest?access_key=1da4394e64da8f692a593dbaae306f28&symbols=" +
                    fromCurrency + "," + toCurrency);

                var data = JsonSerializer.Deserialize<CurrencyResponse>(response);

                if (data.success && data.rates != null)
                {
                    decimal firstCurrency = data.rates[fromCurrency];
                    decimal secondCurrency = data.rates[toCurrency];
                    return amount * (secondCurrency / firstCurrency);
                }
                throw new Exception("Could not convert.");
            }

        }

        public static async Task<decimal> ConvertCurrencyHistorical(DateTime date, string fromCurrency, string toCurrency, decimal amount)
        {
            using (HttpClient client = new())
            {
                string response = await client.GetStringAsync(
                    $"http://data.fixer.io/api/{date:yyyy-MM-dd}?access_key=1da4394e64da8f692a593dbaae306f28&symbols={fromCurrency},{toCurrency}");

                var data = JsonSerializer.Deserialize<CurrencyResponse>(response);

                if (data.historical && data.success && data.rates != null)
                {
                    decimal firstCurrency = data.rates[fromCurrency];
                    decimal secondCurrency = data.rates[toCurrency];
                    return amount * (secondCurrency / firstCurrency);
                }
                throw new Exception("Could not convert.");
            }

        }
    }
}
