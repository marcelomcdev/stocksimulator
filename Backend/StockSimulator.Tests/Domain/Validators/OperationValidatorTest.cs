using System;
using System.Collections.Generic;
using System.Text;
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

        private void ValidateName(Operation model, bool shouldHaveError = false)
        {
            var result = validator.TestValidate(model);
            if (shouldHaveError)
                result.ShouldHaveValidationErrorFor(u => u.Name);
            else
                result.ShouldNotHaveValidationErrorFor(u => u.Name);
        }

        private void ValidateQuantity(Operation model, bool shouldHaveError = false)
        {
            var result = validator.TestValidate(model);
            if (shouldHaveError)
                result.ShouldHaveValidationErrorFor(u => u.Quantity);
            else
                result.ShouldNotHaveValidationErrorFor(u => u.Quantity);
        }

        private void ValidateOperationType(Operation model, bool shouldHaveError = false)
        {
            var result = validator.TestValidate(model);
            if (shouldHaveError)
                result.ShouldHaveValidationErrorFor(u => u.OperationType);
            else
                result.ShouldNotHaveValidationErrorFor(u => u.OperationType);

        }

        #endregion


        #region Name Validation

        [Test]
        public void Should_have_error_when_name_is_null()
        {
            var model = new Operation() { Name = null };
            ValidateName(model, true);
        }

        [Test]
        public void Should_have_error_when_name_is_empty()
        {
            var model = new Operation() { Name = string.Empty };
            ValidateName(model, true);
        }

        [Test]
        public void Should_have_error_when_name_is_lt_3()
        {
            var model = new Operation() { Name = "El" };
            ValidateName(model, true);
        }

        [Test]
        public void Should_have_error_when_name_is_gt_50()
        {
            var name = "O".PadRight(51, 'o');
            var model = new Operation() { Name = name };
            ValidateName(model, true);
        }

        [Test]
        public void Should_have_pass_when_name_has_met_all_requirements()
        {
            var model = new Operation() { Name = "CMIG4" };
            ValidateName(model);
        }

        #endregion

        #region Quantity Validation

        [Test]
        public void Should_have_error_when_quantity_is_zero()
        {
            var model = new Operation() { Quantity = 0 };
            ValidateQuantity(model, true);
        }

        [Test]
        public void Should_pass_when_quantity_is_gt_zero()
        {
            var model = new Operation() { Quantity = 1 };
            ValidateQuantity(model,false);
        }


        #endregion

        #region Operation Type Validation

        [Test]
        public void Should_have_error_when_operation_type_is_zero()
        {
            var model = new Operation() { OperationType = 0 };
            ValidateOperationType(model, true);
        }

        [Test]
        public void Should_pass_when_quantity_is_equals_buy()
        {
            var model = new Operation() { OperationType = 1 };
            ValidateOperationType(model, false);
        }


        [Test]
        public void Should_pass_when_quantity_is_equals_sell()
        {
            var model = new Operation() { OperationType = 2 };
            ValidateOperationType(model, false);
        }


        [Test]
        public void Should_have_error_when_operation_type_is_out_of_range()
        {
            var model = new Operation() { OperationType = 3 };
            ValidateOperationType(model, true);
        }

        #endregion

    }
}
