using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;


namespace WebUi.Tests.Helpers
{
    public class DataContext : ArtMarketPlaceDbContext
    {
        public DataContext(DbContextOptions<ArtMarketPlaceDbContext> optionsBuilder) : base(optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
