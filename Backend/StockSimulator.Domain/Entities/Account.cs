using System.Collections.Generic;

namespace StockSimulator.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string Name { get; set; }
        public decimal TotalBalance { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
    }
}
