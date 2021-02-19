using StockSimulator.CrossCutting.Business;
using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Interfaces.Business;
using StockSimulator.Domain.Interfaces.Repository;
using StockSimulator.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace StockSimulator.Service.Services
{
    public class OperationService : Service<Operation>, IOperationService
    {
        private ITradeOperations _tradeOperations;
        public OperationService(IOperationRepository repository, ITradeOperations tradeOperations) : base(repository)
        {
            _tradeOperations = tradeOperations;
        }

        public int InsertIdentity(Operation entity)
        {
            base.Insert(entity);
            base.Commit();
            return entity.Id;
        }

        public void InsertIdenties(List<Operation> entities)
        {
            base.Insert(entities);
            base.Commit();
        }

        public IEnumerable<dynamic> GetMostTradedOperations()
        {
            var trades = _tradeOperations.GetMostTradedOperations(base.GetAll().ToList());
            return trades;
        }


    }
}
