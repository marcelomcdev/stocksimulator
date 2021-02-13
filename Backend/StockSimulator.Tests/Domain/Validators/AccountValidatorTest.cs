using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FluentValidation;
using FluentValidation.TestHelper;
using StockSimulator.Domain.Validators;
using StockSimulator.Domain.Entities;

namespace StockSimulator.Tests.Domain.Validators
{
    [TestFixture]
    public class AccountValidatorTest
    {
        private AccountValidator validator;
        [SetUp]
        public void Setup()
        {
            validator = new AccountValidator();
        }

        [Test]
        public void Should_not_have_error_when_id_is_zero()
        {
            var model = new Account() { Id = 0 };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(acc => acc.Id);
        }

        public void Should_have_error_when_id_is_lt_zero()
        {
            var model = new Account() { Id = -1 };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(acc => acc.Id);
        }

        [Test]
        public void Should_have_error_when_name_is_null()
        {
            var model = new Account() { Name= null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(acc => acc.Name);
        }

        [Test]
        public void Should_have_error_when_name_is_empty()
        {
            var model = new Account() { Name = "" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(acc => acc.Name);
        }

        [Test]
        public void Should_have_error_when_name_is_gt_20_characters()
        {
            var model = new Account() { Name = "Banco de Desenvolvimento do Estado de Minas Gerais" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(acc => acc.Name);
        }

        [Test]
        public void Should_have_error_when_name_is_lt_3_characters()
        {
            var model = new Account() { Name = "AC" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(acc => acc.Name);
        }

    }
}
