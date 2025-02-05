﻿using System.Text.Json;

namespace Currency_converter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Convert currency");
            Console.Write("From currency:");
            string fromCurrency = Console.ReadLine().ToUpper();

            Console.Write("To currency:");
            string toCurrency = Console.ReadLine().ToUpper();

            Console.Write("Amount to convert:");
            decimal amount = decimal.Parse(Console.ReadLine());

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetStringAsync(
                        "http://data.fixer.io/api/latest?access_key=1da4394e64da8f692a593dbaae306f28&symbols=" +
                        fromCurrency + "," + toCurrency);

                    //Console.WriteLine(response); For å sjekke dataen som sendes fra api

                    var data = JsonSerializer.Deserialize<CurrencyResponse>(response);
                    
                    if (data.success && data.rates != null)
                    {
                        decimal fraKurs = data.rates[fromCurrency];
                        decimal tilKurs = data.rates[toCurrency];
                        decimal resultat = amount * (tilKurs / fraKurs);
                        Console.WriteLine($"resultat: {resultat:F2}");
                    }
                }
                
                
            } catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            

            
        }
    }

    class CurrencyResponse
    {
        public bool success { get; set; }
        public Dictionary<string, decimal> rates { get; set; }
    }


}
