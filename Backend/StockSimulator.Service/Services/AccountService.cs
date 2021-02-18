using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Enums;
using StockSimulator.Domain.Interfaces.Repository;
using StockSimulator.Domain.Interfaces.Services;
using System.Linq;
using static StockSimulator.Domain.Enums.Enumerators;

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


        #region Operations

        public bool CanSell(Account entity, string name, int quantity)
        {
            if (entity == null) return false;

            var operations = entity.Operations.Where(f => f.Name.Equals(name));
            if(operations.Count() > 0)
            {
                var total = operations.Sum(f => f.OperationType == OperationTypeEnum.Buy ? f.Quantity : f.Quantity * (-1));
                return (total >= quantity);
            }

            return false;
        }

        public bool CanSell(int accountId, string name, int quantity)
        {
            return CanSell(base.FindById(accountId), name, quantity);
        }


        public bool CanBuy(Account entity, decimal totalValue)
        {
            if (entity == null) return false;
            return entity.TotalBalance >= totalValue;
        }

        #endregion Operations


        public void Operation(Account entity, Operation operation)
        {
           if(operation.OperationType == OperationTypeEnum.Buy)
            {
                if(CanBuy(entity, operation.TotalValue))
                {
                    //Executa a compra
                }
            }
            else
            {
                if(CanSell(entity, operation.Name, operation.Quantity))
                {
                    //Executa a venda
                }
            }
        }

    }
}
