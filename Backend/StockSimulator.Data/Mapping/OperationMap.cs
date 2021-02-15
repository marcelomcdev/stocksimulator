using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSimulator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Data.Mapping
{
    public class OperationMap : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            builder.Property(o => o.Price).IsRequired().HasColumnType("decimal");
            builder.Property(o => o.Quantity).IsRequired().HasColumnType("int");
            builder.Property(o => o.OperationType).IsRequired().HasColumnType("int");
        }
    }
}
