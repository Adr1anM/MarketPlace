using MarketPlace.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Infrastructure.Persistance.Configurations
{
    public class AuthorAuthorCategoryConfig : IEntityTypeConfiguration<AuthorAuthorCategory>
    {
        public void Configure(EntityTypeBuilder<AuthorAuthorCategory> builder)
        {
            builder.ToTable("AuthorAuthorCategory");


            builder.HasOne(a => a.Author)
                   .WithMany(ac => ac.AuthorAuthorCategories)
                   .HasForeignKey(a => a.AuthorId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(a => a.AuthorCategory)
                   .WithMany(ac => ac.AuthorAuthorCategories)
                   .HasForeignKey(a => a.AuthorCategoryId)
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
