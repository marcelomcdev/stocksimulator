using StockSimulator.Domain.Entities;

namespace StockSimulator.Domain.Interfaces.Services
{
    public interface IUserService : IService<User>
    {
        string InsertIdentity(User entity);
    }
}
