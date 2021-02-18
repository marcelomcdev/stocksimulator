using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSimulator.Domain.Entities;

namespace StockSimulator.Data.Mapping
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Bank).IsRequired().HasColumnType("int");
            builder.Property(a => a.Branch).IsRequired().HasColumnType("int");
            builder.Property(a => a.AccountNumber).IsRequired().HasColumnType("int");
            builder.Property(a => a.UserId).IsRequired();
            builder.Property(a => a.TotalBalance).IsRequired().HasColumnType("decimal(10,2)");

            builder
                .HasMany(a => a.Operations)
                .WithOne(a => a.Account);
        }
    }
}
