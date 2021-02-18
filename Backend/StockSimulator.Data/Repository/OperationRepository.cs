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
    public class OperationRepository : Repository<Operation>, IOperationRepository
    {
        private readonly StockContext context;

        public OperationRepository(StockContext _context) : base(_context)
        {
            context = _context;
        }

        public override IQueryable<Operation> FindBy(Expression<Func<Operation, bool>> predicate)
        {
            IQueryable<Operation> query = context.Operations;
            return query.Where(predicate);
        }

        public override IEnumerable<Operation> GetAll()
        {
            return context.Operations.AsNoTracking().OrderBy(x => x.Symbol);
        }
    }
}