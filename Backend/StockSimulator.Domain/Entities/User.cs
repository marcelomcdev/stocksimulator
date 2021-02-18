using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace StockSimulator.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
    }
}
