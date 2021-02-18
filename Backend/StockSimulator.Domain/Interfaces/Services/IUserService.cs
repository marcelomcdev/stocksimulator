using StockSimulator.Domain.Entities;

namespace StockSimulator.Domain.Interfaces.Services
{
    public interface IUserService : IService<User>
    {
        string InsertIdentity(User entity);
        bool CanSell(User entity, string name, int quantity);
        bool CanSell(string userId, string name, int quantity);
        bool CanBuy(User entity, decimal totalValue);
    }
}
