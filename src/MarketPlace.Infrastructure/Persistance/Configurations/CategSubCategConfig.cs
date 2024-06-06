using MarketPlace.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MarketPlace.Infrastructure.Persistance.Configurations
{
    public class CategSubCategConfig : IEntityTypeConfiguration<CategoriesSubcategories>
    {
        public void Configure(EntityTypeBuilder<CategoriesSubcategories> builder)
        {
            builder.ToTable("CategSubCateg");

            builder.HasOne(c => c.Category)
                   .WithMany(cs => cs.CategoriesSubcategories)
                   .HasForeignKey(c => c.CategoryId);
                 

            builder.HasOne(c => c.SubCategory)
                   .WithMany(cs => cs.CategoriesSubcategories)
                   .HasForeignKey(c => c.SubCategoryId);
                  
        }
    }
}
