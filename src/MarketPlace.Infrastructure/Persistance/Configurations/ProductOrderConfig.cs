using MarketPlace.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Persistance.Configurations
{
    public class ProductOrderConfig : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.ToTable("ProductOrder");

            builder.HasOne(po => po.Product)
                   .WithMany(p => p.ProductOrders)
                   .HasForeignKey(po => po.ProductId);
                   

            builder.HasOne(po => po.Order)
                   .WithMany(o => o.ProductOrders)
                   .HasForeignKey(po => po.OrderId);
        }
    }
}
