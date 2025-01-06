using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MealOrderingApp.Server.Data.Models;

namespace MealOrderingApp.Server.Data.Configuration
{

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.Property(e => e.Description).HasMaxLength(1000);
            builder.Property(e => e.SupplierId).IsRequired();
            builder.Property(e => e.ExpiredDate).IsRequired();
            builder.HasOne(d => d.User)
                   .WithMany(p => p.Orders)
                   .HasForeignKey(d => d.CreateUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Supplier)
               .WithMany(p => p.Orders)
               .HasForeignKey(d => d.SupplierId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
