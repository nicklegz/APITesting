using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TechnicalAnalysisApp_Api.Models
{
    public class StockDataContext : DbContext
    {
        public StockDataContext(): base("StockDataContext")
        {
        }

        public DbSet<Equity> Equities { get; set; }
        public DbSet<Quote> Quotes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}