using StockSimulator.Domain.Entities;

namespace StockSimulator.Domain.Interfaces.Services
{
    public interface IOperationService : IService<Operation>
    {
        int InsertIdentity(Operation entity);
        
    }
}
