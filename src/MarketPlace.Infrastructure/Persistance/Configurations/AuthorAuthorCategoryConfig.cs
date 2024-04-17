using MarketPlace.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
