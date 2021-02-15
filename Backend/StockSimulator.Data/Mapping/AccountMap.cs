﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSimulator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Data.Mapping
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(20).HasColumnType("varchar");
            builder.Property(a => a.UserId).IsRequired();
        }
    }
}