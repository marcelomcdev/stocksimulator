using FluentValidation.TestHelper;
using NUnit.Framework;
using StockSimulator.Domain.Entities;
using StockSimulator.Service.Validators;

namespace StockSimulator.Tests.Service.Validators
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

        #region Related functions

        private void Validate(Account model, System.Linq.Expressions.Expression<System.Func<Account, string>> expression, bool shouldHaveError = false)
        {
            var result = validator.TestValidate(model);
            if (shouldHaveError)
                result.ShouldHaveValidationErrorFor(expression);
            else
                result.ShouldNotHaveValidationErrorFor(expression);
        }

        private void Validate(Account model, System.Linq.Expressions.Expression<System.Func<Account, int>> expression, bool shouldHaveError = false)
        {
            var result = validator.TestValidate(model);
            if (shouldHaveError)
                result.ShouldHaveValidationErrorFor(expression);
            else
                result.ShouldNotHaveValidationErrorFor(expression);
        }

        #endregion

        #region Id Validation

        [Test]
        public void Should_not_have_error_when_id_is_zero()
        {
            var model = new Account() { Id = 0 };
            Validate(model, x => x.Id);
        }

        public void Should_have_error_when_id_is_lt_zero()
        {
            var model = new Account() { Id = -1 };
            Validate(model, x => x.Id);
        }

        #endregion

        #region Bank Validation

        [Test]
        public void Should_have_error_when_bank_is_zero()
        {
            var model = new Account() { Bank = 0 };
            Validate(model, x => x.Bank, true);
        }

        public void Should_have_error_when_bank_is_diferent_of_352()
        {
            var model = new Account() { Id = 100 };
            Validate(model, x => x.Bank, true);
        }

        public void Should_have_error_when_bank_is_eq_352()
        {
            var model = new Account() { Id = 352 };
            Validate(model, x => x.Bank);
        }

        #endregion

    }
}
