﻿using FluentValidation.TestHelper;
using NUnit.Framework;
using StockSimulator.Domain.Entities;
using StockSimulator.Service.Validators;
using static StockSimulator.Domain.Enums.Enumerators;

namespace StockSimulator.Tests.Service.Validators
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

        private void Validate(Operation model, System.Linq.Expressions.Expression<System.Func<Operation, OperationTypeEnum>> expression, bool shouldHaveError = false)
        {
            var result = validator.TestValidate(model);
            if (shouldHaveError)
                result.ShouldHaveValidationErrorFor(expression);
            else
                result.ShouldNotHaveValidationErrorFor(expression);
        }

        #endregion


        #region Symbol Validation

        [Test]
        public void Should_have_error_when_symbol_is_null()
        {
            var model = new Operation() { Symbol = null };
            Validate(model, x => x.Symbol, true);

        }

        [Test]
        public void Should_have_error_when_symbol_is_empty()
        {
            var model = new Operation() { Symbol = string.Empty };
            Validate(model, x => x.Symbol, true);
        }

        [Test]
        public void Should_have_error_when_symbol_is_lt_3()
        {
            var model = new Operation() { Symbol = "El" };
            Validate(model, x => x.Symbol, true);
        }

        [Test]
        public void Should_have_error_when_symbol_is_gt_50()
        {
            var symbol = "O".PadRight(51, 'o');
            var model = new Operation() { Symbol = symbol };
            Validate(model, x => x.Symbol, true);
        }

        [Test]
        public void Should_have_pass_when_symbol_has_met_all_requirements()
        {
            var model = new Operation() { Symbol = "CMIG4" };
            Validate(model, x => x.Symbol);
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
        public void Should_pass_when_quantity_is_equals_buy()
        {
            var model = new Operation() { OperationType = OperationTypeEnum.Buy };
            Validate(model, x => x.OperationType, false);
        }

        [Test]
        public void Should_pass_when_quantity_is_equals_sell()
        {
            var model = new Operation() { OperationType = OperationTypeEnum.Sell };
            Validate(model, x => x.OperationType, false);
        }

        #endregion

    }
}
