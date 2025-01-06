using MealOrderingApp.Server.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrderingApp.Server.Data.Configuration
{

    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.Property(e => e.WebUrl).HasMaxLength(500);


        }
    }
}
