using StockSimulator.Domain.Entities;

namespace StockSimulator.Domain.Interfaces.Services
{
    public interface IAccountService : IService<Account>
    {
        int InsertIdentity(Account entity);
        int GetLastAccountNumber();
    }
}
