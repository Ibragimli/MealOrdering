using MealOrderingApp.Server.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrderingApp.Server.Data.Configuration
{
    
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
           
            builder.Property(e => e.Description).HasMaxLength(1000);

            builder.HasOne(d => d.Order)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.User)
               .WithMany(p => p.CreatedOrderItems)
               .HasForeignKey(d => d.CreateUserId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
