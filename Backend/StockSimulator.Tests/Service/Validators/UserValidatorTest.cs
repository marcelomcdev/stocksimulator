﻿using FluentValidation.TestHelper;
using NUnit.Framework;
using StockSimulator.Domain.Entities;
using StockSimulator.Service.Validators;

namespace StockSimulator.Tests.Service.Validators
{
    [TestFixture]
    public class UserValidatorTest
    {
        private UserValidator validator;
        [SetUp]
        public void Setup()
        {
            validator = new UserValidator();
        }

        #region Related functions

        private void Validate(User model, System.Linq.Expressions.Expression<System.Func<User,string>> expression, bool shouldHaveError = false)
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
            var model = new User() { Name = null };
            Validate(model, u => u.Name, true);
        }

        [Test]
        public void Should_have_error_when_name_is_empty()
        {
            var model = new User() { Name = string.Empty };
            Validate(model, u => u.Name, true);
        }

        [Test]
        public void Should_have_error_when_name_is_lt_3()
        {
            var model = new User() { Name = "AB" };
            Validate(model, u => u.Name, true);
        }

        [Test]
        public void Should_have_error_when_name_is_gt_100()
        {
            var name = "A".PadRight(101, 'a');
            var model = new User() { Name = name };
            Validate(model, u => u.Name, true);
        }

        [Test]
        public void Should_have_pass_when_name_has_met_all_requirements()
        {
            var model = new User() { Name = "Gabriela de Souza Pereira" };
            Validate(model, u => u.Name);
        }

        #endregion

        #region Email Validation

        [Test]
        public void Should_have_error_when_email_is_null()
        {
            var model = new User() { Email = null };
            Validate(model, u => u.Email, true);
        }

        [Test]
        public void Should_have_error_when_email_is_empty()
        {
            var model = new User() { Email = "" };
            Validate(model, u => u.Email, true);
        }

        [Test]
        public void Should_have_error_when_email_has_an_invalid_format()
        {
            var model = new User() { Email = "marcelo.castro" };
            Validate(model, u => u.Email, true);
        }

        [Test]
        public void Should_have_a_valid_mail()
        {
            var model = new User() { Email = "marcelo.developer@outlook.com" };
            Validate(model, u => u.Email);
        }

        //[Test]
        //public void Should_have_error_when_email_is_lt_3()
        //{
        //    Assert.Fail();
        //}

        //[Test]
        //public void Should_have_error_when_email_is_gt_150()
        //{
        //    Assert.Fail();
        //}

        #endregion

    }
}
