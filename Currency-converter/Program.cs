using System.Text.Json;

namespace Currency_converter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
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
                decimal amount = decimal.Parse(Console.ReadLine());

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
                        } else
                        {
                            decimal result = await ConvertCurrencyHistorical(date, fromCurrency, toCurrency, amount);
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
                        decimal result = await ConvertCurrency(fromCurrency, toCurrency, amount);
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

        static async Task<decimal> ConvertCurrency(string fromCurrency, string toCurrency, decimal amount)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(
                    "http://data.fixer.io/api/latest?access_key=1da4394e64da8f692a593dbaae306f28&symbols=" +
                    fromCurrency + "," + toCurrency);      

                var data = JsonSerializer.Deserialize<CurrencyResponse>(response);

                if (data.success && data.rates != null)
                {
                    decimal fraKurs = data.rates[fromCurrency];
                    decimal tilKurs = data.rates[toCurrency];
                    return amount * (tilKurs / fraKurs);
                }
                throw new Exception("Could not convert.");
            }

        }

        static async Task<decimal> ConvertCurrencyHistorical(DateTime date, string fromCurrency, string toCurrency, decimal amount)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(
                    $"http://data.fixer.io/api/{date:yyyy-MM-dd}?access_key=1da4394e64da8f692a593dbaae306f28&symbols={fromCurrency},{toCurrency}");

                var data = JsonSerializer.Deserialize<CurrencyResponse>(response);

                if (data.historical && data.success && data.rates != null)
                {                   
                    decimal fraKurs = data.rates[fromCurrency];
                    decimal tilKurs = data.rates[toCurrency];
                    return amount * (tilKurs / fraKurs);
                }
                throw new Exception("Could not convert.");
            }

        }


    }

    class CurrencyResponse
    {
        public bool success { get; set; }
        public bool historical { get; set; }
        public Dictionary<string, decimal> rates { get; set; }
    }


}
