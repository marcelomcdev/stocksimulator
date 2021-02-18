using System.Collections.Generic;
using System.ComponentModel;

namespace StockSimulator.Domain.Entities
{
    public class Account : BaseEntity
    {
        public int Bank { get; set; }
        public int Branch { get; set; }
        public int AccountNumber { get; set; }
        public decimal TotalBalance { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
    }
}
