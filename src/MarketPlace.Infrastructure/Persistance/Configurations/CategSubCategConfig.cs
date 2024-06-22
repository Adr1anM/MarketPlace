using MarketPlace.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MarketPlace.Infrastructure.Persistance.Configurations
{
    internal class CategSubCategConfig : IEntityTypeConfiguration<CategorySubcategory>
    {
        public void Configure(EntityTypeBuilder<CategorySubcategory> builder)
        {

            builder.HasOne(psc => psc.Category)
                   .WithMany(p => p.CategorySubcategories)
                   .HasForeignKey(psc => psc.CategoryId);

            builder.HasOne(psc => psc.SubCategory)
                   .WithMany(sc => sc.CategorySubcategories)
                   .HasForeignKey(psc => psc.SubCategoryId);

        }
    }
}
