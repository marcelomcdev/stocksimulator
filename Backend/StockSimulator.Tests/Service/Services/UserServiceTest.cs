using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using StockSimulator.Domain.Interfaces.Services;
using StockSimulator.Service.Services;
using StockSimulator.Domain.Interfaces.Repository;
using StockSimulator.Domain.Entities;

namespace StockSimulator.Tests.Service.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        private IUserRepository userRepository;
        private IUserService userService;

        //private Mock<IUserService> mockService;
        //private Mock<IUserRepository> mockRepository;
        private Mock<IUserRepository> mockRepository;

        

        [SetUp]
        public void Setup()
        {
            userRepository = new Mock<IUserRepository>().Object;
            userService = new Mock<IUserService>().Object;
        }

        [Test]
        [TestCase("marcelo.developer@outlook.com","1234567")]
        public void Should_pass_when_find_a_user(string email, string password)
        {
            
            //var expected = "Marcelo Castro";
            //var service = new Mock<IUserService>();
            //User u = new User() { Email = "marcelo.developer@outlook.com", Name = "Marcelo Castro", Password = "123456", Accounts = null, Id = 3 };
            //var result = service.Setup(f => f.Authenticate(email, password)).Returns(u);
            
            //Assert.AreEqual(u.Name, expected);
            
            Assert.Fail();
        }

        [Test]
        public void Should_have_error_when_not_find_a_user()
        {
            Assert.Fail();
        }

        [Test]
        public void Should_have_error_when_a_password_is_wrong()
        {
            Assert.Fail();
        }

    }
}
