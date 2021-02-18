using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Interfaces.Repository;
using StockSimulator.Domain.Interfaces.Services;
using System.Linq;

namespace StockSimulator.Service.Services
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {

        }

        public string InsertIdentity(User entity)
        {
            base.Insert(entity);
            base.Commit();
            return entity.Id;
        }
    }
}
