using Microsoft.EntityFrameworkCore;
using StockSimulator.Data.Context;
using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace StockSimulator.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly StockContext context;

        public UserRepository(StockContext _context) : base(_context)
        {
            context = _context;
        }

        public override IQueryable<User> FindBy(Expression<Func<User, bool>> predicate)
        {
            IQueryable<User> query = _context.Users
                .Include("Accounts");
            return query.Where(predicate);
        }

        public override IEnumerable<User> GetAll()
        {
            return _context.Users.Include("Accounts").AsNoTracking().OrderBy(x => x.Name);
        }
    }
}
