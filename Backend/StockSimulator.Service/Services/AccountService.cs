using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Interfaces.Repository;
using StockSimulator.Domain.Interfaces.Services;
using System.Linq;

namespace StockSimulator.Service.Services
{
    public class AccountService : Service<Account>, IAccountService
    {
        public AccountService(IAccountRepository repository) : base(repository)
        {

        }

        public int GetLastAccountNumber()
        {
            var query = base.GetAll().OrderBy(f => f.AccountNumber).LastOrDefault();
            if (query != null)
                return query.AccountNumber;
            else
                return 0;
        }

        public int InsertIdentity(Account entity)
        {
            base.Insert(entity);
            base.Commit();
            return entity.Id;
        }




    }
}
