using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSimulator.Domain.Entities;

namespace StockSimulator.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.ToTable("Users");
           
            builder.HasKey(u => u.Id);
            //builder.Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100).HasColumnType("varchar");
            builder.Property(u => u.Email).IsRequired().HasMaxLength(150).HasColumnType("varchar");
            builder.Property(u => u.Password).IsRequired().HasMaxLength(20).HasColumnType("varchar");

            builder.HasMany(u => u.Accounts).WithOne(u => u.User);
        }
    }
}
