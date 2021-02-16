using StockSimulator.Domain.Entities;

namespace StockSimulator.Domain.Interfaces.Services
{
    public interface IUserService : IService<User>
    {
        User Authenticate(string email,string password);
        /// <summary>
        /// Insert a new instance and returns generated database id
        /// </summary>
        /// <param name="entity">A user entity</param>
        /// <returns></returns>
        string InsertIdentity(User entity);
    }
}
