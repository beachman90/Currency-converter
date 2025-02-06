using Currency_converter.Models;
using Microsoft.EntityFrameworkCore;

namespace Currency_converter.Data
{
    public class CurrencyContext : DbContext
    {
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CurrencyDb;Trusted_Connection=True;");
        }
    }
}
