using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSimulator.Domain.Entities;

namespace StockSimulator.Data.Mapping
{
    public class OperationMap : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Symbol).IsRequired().HasMaxLength(50).HasColumnType("varchar(50)");
            builder.Property(o => o.CurrentPrice).IsRequired().HasColumnType("decimal(10,2)");
            builder.Property(o => o.Quantity).IsRequired().HasColumnType("int");
            builder.Property(o => o.OperationType).IsRequired().HasColumnType("int");
        }
    }
}
