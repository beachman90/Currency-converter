using Currency_converter.Data;
using Currency_converter.Models;
using System.Text.Json;
using Currency_converter.Services;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Currency_converter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            ICurrencyService currencyService = new CurrencyService();

            //Umiddelbar sjekk av lagrede rater

            //await currencyService.StoreDailyRates();

            //using (var context = new CurrencyContext())
            //{
            //    var storedRates = context.ExchangeRates.ToList();
            //    Console.WriteLine("Lagrede valutakurser:");
            //    foreach (var rate in storedRates)
            //    {
            //        Console.WriteLine($"Valuta: {rate.Currency}, Kurs: {rate.Rate}, Dato: {rate.Date}");
            //    }
            //}


            bool continueProgram = true;
            while (continueProgram)
            {


                Console.WriteLine("Convert currency");
                Console.WriteLine(new string('-', 20));
                Console.Write("From currency:");
                string fromCurrency = Console.ReadLine().ToUpper();

                Console.Write("To currency:");
                string toCurrency = Console.ReadLine().ToUpper();

                Console.Write("Amount to convert:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    Console.WriteLine("Invalid amount. Please enter a valid number.");
                    continue;
                }
                Console.Write("Would you like to see the exchange rate for a specifikc date? yes/no:");
                if (Console.ReadLine().ToLower() == "yes")
                {
                    Console.WriteLine("Enter date (YYYY-MM-DD)");

                    try
                    {
                        DateTime date = DateTime.Parse(Console.ReadLine());

                        if (date > DateTime.Now)
                        {
                            Console.WriteLine("I'm no fortune teller.");
                            return;
                        }
                        else
                        {
                            decimal result = await currencyService.ConvertCurrencyHistorical(date, fromCurrency, toCurrency, amount);
                            Console.WriteLine($"Result: {result:F2}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Please enter a valid date (YYYY-MM-DD");
                    }

                }
                else
                {
                    try
                    {
                        decimal result = await currencyService.ConvertCurrency(fromCurrency, toCurrency, amount);
                        Console.WriteLine($"Result: {result:F2}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                }

                Console.WriteLine("\nWould you like to convert again? (yes/no)");
                continueProgram = Console.ReadLine().ToLower() == "yes";

                if (continueProgram)
                    Console.WriteLine();
            }

        }                     

    }
    
}
