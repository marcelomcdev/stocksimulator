using Coderful.EntityFramework.Testing.Mock;
using Moq;
using StockSimulator.Data.Context;
using StockSimulator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace StockSimulator.Tests.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public virtual DbSet<User> Users { get; set; }
    }

}
