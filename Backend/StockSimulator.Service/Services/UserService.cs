using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Interfaces.Repository;
using StockSimulator.Domain.Interfaces.Services;
using System.Linq;
using static StockSimulator.Domain.Enums.Enumerators;

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


        #region Operations

        public bool CanSell(User entity, string name, int quantity)
        {
            if (entity == null) return false;

            var operations = entity.Operations.Where(f => f.Symbol.Equals(name));
            if (operations.Count() > 0)
            {
                var total = operations.Sum(f => f.OperationType == OperationTypeEnum.Buy ? f.Quantity : f.Quantity * (-1));
                return (total >= quantity);
            }

            return false;
        }

        public bool CanSell(string userId, string name, int quantity)
        {
            return CanSell(base.FindBy(u=> u.Id == userId).FirstOrDefault(), name, quantity);
        }


        public bool CanBuy(User entity, decimal totalValue)
        {
            if (entity == null) return false;
            return (entity.Accounts?.FirstOrDefault()?.TotalBalance ?? 0) >= totalValue;
        }

        #endregion Operations


        public void Operation(User entity, Operation operation)
        {
            if (operation.OperationType == OperationTypeEnum.Buy)
            {
                if (CanBuy(entity, operation.TotalValue))
                {
                    //Executa a compra
                    
                }
            }
            else
            {
                if (CanSell(entity, operation.Symbol, operation.Quantity))
                {
                    //Executa a venda
                }
            }
        }

    }
}
