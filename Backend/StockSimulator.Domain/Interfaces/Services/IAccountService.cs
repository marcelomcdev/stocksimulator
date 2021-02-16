using StockSimulator.Domain.Entities;

namespace StockSimulator.Domain.Interfaces.Services
{
    public interface IAccountService : IService<Account>
    {
        int InsertIdentity(Account entity);
        bool CanSell(Account entity, string name, int quantity);
        bool CanSell(int accountId, string name, int quantity);
    }
}
