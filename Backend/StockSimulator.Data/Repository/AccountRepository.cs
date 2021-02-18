using Microsoft.EntityFrameworkCore;
using StockSimulator.Data.Context;
using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StockSimulator.Data.Repository
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private readonly StockContext context;

        public AccountRepository(StockContext _context) : base(_context)
        {
            context = _context;
        }

        public override IQueryable<Account> FindBy(Expression<Func<Account, bool>> predicate)
        {
            IQueryable<Account> query = context.Accounts.Include("Operations");
            return query.Where(predicate);
        }

        public override IEnumerable<Account> GetAll()
        {
            return context.Accounts.Include("Operations").AsNoTracking().OrderBy(x => x.Bank).ThenBy(f=> f.Branch).ThenBy(f=> f.AccountNumber);
        }
    }
}