using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using StockSimulator.Data.Context;
using StockSimulator.Data.Repository;
using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Interfaces.Repository;
using StockSimulator.Domain.Interfaces.Services;
using StockSimulator.Service.Services;
using StockSimulator.Tests.Data;
using StockSimulator.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static StockSimulator.Domain.Enums.Enumerators;

namespace StockSimulator.Tests.Service.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        const string InternalUserId = "892a3480-d9df-4356-9e36-2f1531ad85c9";

        private Mock<IUserService> _mockUserService;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IOperationRepository> _mockOperationRepository;
        private UserService service;
        [SetUp]
        public void Setup()
        {
            _mockOperationRepository = new Mock<IOperationRepository>();
            _mockUserRepository = new Mock<IUserRepository>();

            _mockUserService = new Mock<IUserService>();
            _mockUserService.Setup(f => f.CanBuy(It.IsAny<User>(), It.IsAny<decimal>())).Returns(true).Verifiable();

            service = new UserService(_mockUserRepository.Object, _mockOperationRepository.Object);

        }


        [Test]
        [TestCase(500)]
        [TestCase(400)]
        [TestCase(300)]
        public void ShouldNotBuyWhenTotalAccountIsLtTotalBalance(int totalBalance)
        {
            List<Acao> acoes = new List<Acao>() { 
                new Acao() { Quantity = 120, Value = 2.85M, Operation = OperationTypeEnum.Buy },
                new Acao() { Quantity = 43, Value = 2.85M, Operation = OperationTypeEnum.Buy },
                new Acao() { Quantity = 18, Value = 2.85M, Operation = OperationTypeEnum.Buy },
                new Acao() { Quantity = 25, Value = 2.85M, Operation = OperationTypeEnum.Buy },
                new Acao() { Quantity = 32, Value = 2.85M, Operation = OperationTypeEnum.Buy },
                new Acao() { Quantity = 44, Value = 2.85M, Operation = OperationTypeEnum.Buy },
            };

            var context = MockStockContext.GenerateContext(InternalUserId, totalBalance, acoes);
            var user = context.Users.FirstOrDefault(f => f.Id == InternalUserId);
            decimal total = context.Operations.Where(f => f.OperationType == OperationTypeEnum.Buy).ToList().Sum(f => f.TotalValue);
            
            _mockUserService.Setup(f => f.CanBuy(It.IsAny<User>(), total)).Returns(false);
            var canBuy = _mockUserService.Object.CanBuy(user, total);

            var verif = new Mock<IUserService>().Object;
            var result = verif.CanBuy(user, total);

            Assert.IsFalse(result);
            Assert.AreEqual(canBuy, result);
        }

        [Test]
        public void ShouldHaveBuyWhenTotalAccountIs2000([Values(2.85, 12.45, 2.95)] decimal preco, [Range(120, 120, 120)] int qte)
        {
            var context = MockStockContext.GenerateContext(InternalUserId,2000);
            var firstOperation = context.Operations.FirstOrDefault();
            firstOperation.CurrentPrice = preco;
            firstOperation.Quantity = qte;

            var user = context.Users.FirstOrDefault(f => f.Id == InternalUserId);
            decimal total = context.Operations.Where(f => f.OperationType == OperationTypeEnum.Buy).ToList().Sum(f => f.TotalValue);

            _mockUserService.Setup(f => f.CanBuy(It.IsAny<User>(), total)).Returns(true);
            var canBuy = _mockUserService.Object.CanBuy(user, total);

            var result = service.CanBuy(user, total);

            Assert.IsTrue(result);
            Assert.AreEqual(canBuy, result);
        }

        [Test]
        public void ShouldNotSellWhenHaveMinusTradesThanRequested()
        {
            List<Acao> acoes = new List<Acao>() {
                new Acao() { Quantity = 120, Value = 2.85M, Operation = OperationTypeEnum.Sell },
                new Acao() { Quantity = 43, Value = 2.85M, Operation = OperationTypeEnum.Sell },
                new Acao() { Quantity = 18, Value = 2.85M, Operation = OperationTypeEnum.Sell },
                new Acao() { Quantity = 25, Value = 2.85M, Operation = OperationTypeEnum.Sell },
                new Acao() { Quantity = 32, Value = 2.85M, Operation = OperationTypeEnum.Sell },
                new Acao() { Quantity = 44, Value = 2.85M, Operation = OperationTypeEnum.Sell },
            };

            int quantity = 200;
            var context = MockStockContext.GenerateContext(InternalUserId, 2000);
            var user = context.Users.FirstOrDefault(f => f.Id == InternalUserId);

            _mockUserService.Setup(f => f.CanSell(It.IsAny<User>(), It.IsAny<string>(), quantity)).Returns(false);
            
            var canBuy = _mockUserService.Object.CanSell(user, "VAL3", quantity);
            var result = service.CanSell(user, "VAL3", quantity);

            Assert.IsFalse(result);
            Assert.AreEqual(canBuy, result);
        }

    }
}
