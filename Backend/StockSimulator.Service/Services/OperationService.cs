using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Interfaces.Repository;
using StockSimulator.Domain.Interfaces.Services;

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
    }
}
