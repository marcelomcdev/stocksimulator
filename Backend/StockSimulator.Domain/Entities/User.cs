using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        
        /// <summary>
        /// A user can have one or more accounts
        /// </summary>
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
