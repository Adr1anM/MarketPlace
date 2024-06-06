using MarketPlace.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MarketPlace.Infrastructure.Persistance.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.Author)
                   .WithMany(p => p.Products)
                   .HasForeignKey(p => p.AuthorId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
