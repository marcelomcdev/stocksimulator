using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// A user can have one or more accounts
        /// </summary>
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
