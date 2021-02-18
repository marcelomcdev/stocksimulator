using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSimulator.Domain.Entities;

namespace StockSimulator.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100).HasColumnType("varchar(100)");
            builder.Property(u => u.Email).IsRequired().HasMaxLength(150).HasColumnType("varchar(150)");
            builder.Property(u => u.CPF).IsRequired().HasMaxLength(11).HasColumnType("varchar(11)");

            builder
                .HasMany(u => u.Accounts)
                .WithOne(u => u.User);

            builder
                .HasMany(u => u.Operations)
                .WithOne(u => u.User);
        }
    }
}
