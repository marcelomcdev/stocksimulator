using FluentValidation.TestHelper;
using NUnit.Framework;
using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Validators;

namespace StockSimulator.Tests.Domain.Validators
{
    [TestFixture]
    public class OperationValidatorTest
    {
        private OperationValidator validator;
        [SetUp]
        public void Setup()
        {
            validator = new OperationValidator();
        }

        #region Related functions

        private void Validate(Operation model, System.Linq.Expressions.Expression<System.Func<Operation, string>> expression, bool shouldHaveError = false)
        {
            var result = validator.TestValidate(model);
            if (shouldHaveError)
                result.ShouldHaveValidationErrorFor(expression);
            else
                result.ShouldNotHaveValidationErrorFor(expression);
        }

        private void Validate(Operation model, System.Linq.Expressions.Expression<System.Func<Operation, int>> expression, bool shouldHaveError = false)
        {
            var result = validator.TestValidate(model);
            if (shouldHaveError)
                result.ShouldHaveValidationErrorFor(expression);
            else
                result.ShouldNotHaveValidationErrorFor(expression);
        }

        #endregion


        #region Name Validation

        [Test]
        public void Should_have_error_when_name_is_null()
        {
            var model = new Operation() { Name = null };
            Validate(model, x => x.Name, true);

        }

        [Test]
        public void Should_have_error_when_name_is_empty()
        {
            var model = new Operation() { Name = string.Empty };
            Validate(model, x => x.Name, true);
        }

        [Test]
        public void Should_have_error_when_name_is_lt_3()
        {
            var model = new Operation() { Name = "El" };
            Validate(model, x => x.Name, true);
        }

        [Test]
        public void Should_have_error_when_name_is_gt_50()
        {
            var name = "O".PadRight(51, 'o');
            var model = new Operation() { Name = name };
            Validate(model, x => x.Name, true);
        }

        [Test]
        public void Should_have_pass_when_name_has_met_all_requirements()
        {
            var model = new Operation() { Name = "CMIG4" };
            Validate(model, x => x.Name);
        }

        #endregion

        #region Quantity Validation

        [Test]
        public void Should_have_error_when_quantity_is_zero()
        {
            var model = new Operation() { Quantity = 0 };
            Validate(model, x => x.Quantity, true);
        }

        [Test]
        public void Should_pass_when_quantity_is_gt_zero()
        {
            var model = new Operation() { Quantity = 1 };
            Validate(model, x => x.Quantity, false);
        }


        #endregion

        #region Operation Type Validation

        [Test]
        public void Should_have_error_when_operation_type_is_zero()
        {
            var model = new Operation() { OperationType = 0 };
            Validate(model, x => x.OperationType, true);
        }

        [Test]
        public void Should_pass_when_quantity_is_equals_buy()
        {
            var model = new Operation() { OperationType = 1 };
            Validate(model, x => x.OperationType, false);
        }


        [Test]
        public void Should_pass_when_quantity_is_equals_sell()
        {
            var model = new Operation() { OperationType = 2 };
            Validate(model, x => x.OperationType, false);
        }


        [Test]
        public void Should_have_error_when_operation_type_is_out_of_range()
        {
            var model = new Operation() { OperationType = 3 };
            Validate(model, x => x.OperationType, true);
        }

        #endregion

    }
}
