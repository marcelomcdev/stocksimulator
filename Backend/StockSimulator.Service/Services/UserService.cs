using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Interfaces.Repository;
using StockSimulator.Domain.Interfaces.Services;
using StockSimulator.Service.QuoteSimulator;
using System.Collections.Generic;
using System.Linq;
using static StockSimulator.Domain.Enums.Enumerators;

namespace StockSimulator.Service.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IOperationRepository _operationRepository;
        public UserService(IUserRepository repository, IOperationRepository operationRepository) : base(repository)
        {
            _operationRepository = operationRepository;
        }

        public string InsertIdentity(User entity)
        {
            base.Insert(entity);
            base.Commit();
            return entity.Id;
        }



        #region Operations

        public bool CanSell(User entity, string name, int quantity, bool validateOperations)
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
            var user = base.FindBy(u => u.Id == userId).FirstOrDefault();
            return CanSell(base.FindBy(u=> u.Id == userId).FirstOrDefault(), name, quantity);
        }

        public bool CanSell(User entity, string name, int quantity)
        {            
            return CanSell(entity, name, quantity, true);
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
                    //inclui uma nova ação na lista e decrementa valor na conta
                    var account = entity.Accounts.FirstOrDefault();
                    if (account.TotalBalance >= operation.TotalValue)
                    {
                        account.TotalBalance = (account.TotalBalance - operation.TotalValue);
                        _operationRepository.Insert(operation);
                        _operationRepository.Commit();
                    }
                }
            }
            else
            {
                if (CanSell(entity, operation.Symbol, operation.Quantity))
                {
                    //Executa a venda
                    //verifica se existe ação
                    //se não houver, lança erro
                    //se houver e não tiver quantidade, lança erro
                    //se houver, diminuir valor e atualizar.
                }
            }
        }

    }
}
