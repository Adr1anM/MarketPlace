using MarketPlace.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace MarketPlace.Infrastructure.Persistance.Configurations
{
    public class ProductSubCategoryConfig : IEntityTypeConfiguration<ProductSubCategory>
    {
        public void Configure(EntityTypeBuilder<ProductSubCategory> builder)
        {
    
            builder.HasOne(psc => psc.Product)
                   .WithMany(p => p.ProductSubcategories)
                   .HasForeignKey(psc => psc.ProductId);

            builder.HasOne(psc => psc.SubCategory)
                   .WithMany(sc => sc.ProductSubcategories)
                   .HasForeignKey(psc => psc.SubCategoryId);

        }
    }
}
