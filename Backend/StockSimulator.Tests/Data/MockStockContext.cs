using Microsoft.EntityFrameworkCore;
using StockSimulator.Data.Context;
using StockSimulator.Domain.Entities;
using StockSimulator.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockSimulator.Tests.Data
{
    public static class MockStockContext
    {
        const string InternalUserId = "892a3480-d9df-4356-9e36-2f1531ad85c9";
        public static StockContext GenerateContext(string userId, decimal totalBalance, List<Acao> acoes = null)
        {
            if (string.IsNullOrEmpty(userId))
                userId = InternalUserId;

            var options = new DbContextOptionsBuilder<StockContext>()
                       .UseInMemoryDatabase(databaseName: "StockContextDatabase")
                       .Options;

            StockContext context = new StockContext(options);

            if (!context.Users.Any(f => f.Id == userId))
            {
                context.Users.Add(new User
                {
                    Id = userId,
                    Email = "marcelo.developer@outlook.com"
                });

                context.Accounts.Add(new Account() { UserId = userId, Bank = 352, AccountNumber = 3, TotalBalance = totalBalance });

                List<Operation> operations;

                if (acoes != null)
                {
                    operations = new List<Operation>();
                    foreach (var item in acoes)
                    {
                        var op = new Operation() { UserId = userId, Symbol = item.Symbol ?? "VAL3", CurrentPrice = item.Value, Quantity = item.Quantity, OperationType = item.Operation, OperationDate = DateTime.Now };
                        operations.Add(op);
                    }
                }
                else
                {
                    operations = new List<Operation>()
                    {
                        new Operation() { UserId = userId, Symbol = "VAL3", CurrentPrice = 2.85M, Quantity = 120, OperationType = Domain.Enums.Enumerators.OperationTypeEnum.Buy, OperationDate = DateTime.Now },
                        new Operation() { UserId = userId, Symbol = "VAL3", CurrentPrice = 3.48M, Quantity = 10, OperationType = Domain.Enums.Enumerators.OperationTypeEnum.Buy, OperationDate = DateTime.Now },
                        new Operation() { UserId = userId, Symbol = "VAL3", CurrentPrice = 7.25M, Quantity = 15, OperationType = Domain.Enums.Enumerators.OperationTypeEnum.Sell, OperationDate = DateTime.Now },
                        new Operation() { UserId = userId, Symbol = "VAL3", CurrentPrice = 4.44M, Quantity = 18, OperationType = Domain.Enums.Enumerators.OperationTypeEnum.Sell, OperationDate = DateTime.Now },
                        new Operation() { UserId = userId, Symbol = "VAL3", CurrentPrice = 1.85M, Quantity = 1, OperationType = Domain.Enums.Enumerators.OperationTypeEnum.Buy, OperationDate = DateTime.Now },
                    };
                }

                context.Operations.AddRange(operations);
                context.SaveChanges();
            }

            if (context.Users.First().Accounts == null)
            {
                context.Users.First().Accounts = new List<Account>();
                var account = context.Accounts.First(f => f.UserId == userId);
                account.TotalBalance = totalBalance;
                context.Users.First().Accounts.Add(account);
            }

            return context;
        }
    }
}
