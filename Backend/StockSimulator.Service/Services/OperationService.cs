using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Interfaces.Repository;
using StockSimulator.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace StockSimulator.Service.Services
{
    public class OperationService : Service<Operation>, IOperationService
    {
        public OperationService(IOperationRepository repository) : base(repository)
        {

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
    }
}
