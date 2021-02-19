using StockSimulator.Domain.Entities;
using System.Collections.Generic;

namespace StockSimulator.Domain.Interfaces.Services
{
    public interface IOperationService : IService<Operation>
    {
        int InsertIdentity(Operation entity);
        void InsertIdenties(List<Operation> entities);
    }
}
