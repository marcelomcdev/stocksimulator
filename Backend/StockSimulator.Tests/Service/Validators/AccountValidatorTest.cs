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

        #region Name Validation

        [Test]
        public void Should_have_error_when_name_is_null()
        {
            var model = new Account() { Name= null };
            Validate(model, x => x.Name, true);

        }

        [Test]
        public void Should_have_error_when_name_is_empty()
        {
            var model = new Account() { Name = "" };
            Validate(model, x => x.Name, true);
        }

        [Test]
        public void Should_have_error_when_name_is_gt_20_characters()
        {
            var model = new Account() { Name = "Banco de Desenvolvimento do Estado de Minas Gerais" };
            Validate(model, x => x.Name, true);
        }

        [Test]
        public void Should_have_error_when_name_is_lt_3_characters()
        {
            var model = new Account() { Name = "AC" };
            Validate(model, x => x.Name, true);
        }

        #endregion

    }
}
